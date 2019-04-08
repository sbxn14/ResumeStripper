using ResumeStripper.DAL;
using ResumeStripper.Filters;
using ResumeStripper.Helpers;
using ResumeStripper.Models;
using ResumeStripper.Models.Viewmodels;
using System.Data.Entity.Validation;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ResumeStripper.Controllers
{
    public class CVController : Controller
    {
        public readonly CvRepository Repo = new CvRepository(new StripperContext());

        [HttpGet]
        [WhitespaceFilter]
        [CompressFilter]
        public ActionResult Index()
        {
            MessageViewModel model = (MessageViewModel)TempData["Message"];
            if (model != null)
            {
                string filename = (string)TempData["file"];

                if (filename.Contains(":"))
                {
                    //Makes filename Microsoft Edge compatible if needed
                    filename = Path.GetFileName(filename);
                }

                //TODO: een betere manier voor filehosting en al dan de manier die nu gebruikt wordt met http-server npm..
                model.ServerPath = "http://192.168.86.27:8081/" + filename;
                ViewBag.JavaScriptFunction = "newPDFArrived('" + model.ServerPath + "');";
                return View(model);
            }
            return View(new MessageViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase pdf)
        {
            //select file button has been pressed
            //if (Request.Files.Count > 0)
            if (pdf.ContentLength > 0)
            {
                string extension = Path.GetExtension(pdf.FileName)?.ToUpper();
                //checks if file is pdf, if not will give error
                if (extension != null && extension.Equals(".PDF"))
                {
                    //empty PDF, return to index
                    if (pdf.ContentLength <= 0)
                    {
                        return RedirectToAction("Index");
                    }

                    TempData["file"] = pdf.FileName;

                    MessageViewModel mod = new MessageViewModel
                    {
                        Path = pdf.FileName
                    };

                    //TODO CALL EXTRACTION METHOD if we go that route

                    TempData["Message"] = mod;
                }
                else
                {
                    ViewBag.FileStatus = "Invalid File Format.";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Export(MessageViewModel model, string submitter)
        {
            if (model.ResultCv == null)
            {
                return RedirectToAction("Index");
            }

            CV cv = model.ResultCv;
            //removes any accidental whitespaces at beginning and end of every value in the CV
            cv.TrimEverything();

            //checks if Languages are using simple or detailed input
            cv.SetLanguageSetting();

            try
            {
                //TODO: improve saving, more specific instead of just saving the entire thing in the database as is.
                //Save (for now) the complete CV-model in the Database
                Repo.Add(cv);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine(@"Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }

            //checks based on submitter name (which button was pressed) if CV should be anonymous or not
            if (submitter.Equals("Generate Anonymous CV"))
            {
                //requires anonymous PDF
                cv.SetAnonymousCv();
            }

            string resultName = "";

            //set name of generated pdf based on if anonymous and if name contains prefix
            if (cv.IsAnonymous)
            {
                //cv is anonymous so file should be too
                resultName = $"CV_User_{cv.ID}_EHV.pdf";
            }
            else
            {
                if (cv.Prefix == "")
                {
                    resultName = $"CV_{cv.Name}_{cv.Surname}_EHV.pdf";
                }
                else
                {
                    resultName = $"CV_{cv.Name}_{cv.Prefix}_{cv.Surname}_EHV.pdf";
                }
            }

            TempData["pdfName"] = resultName;
            //generate PDF and generate view that shows that PDF instead of redirect to index
            //string url = Server.MapPath("~/PDFs/") + resultName;

            PdfHelper helper = new PdfHelper();

            byte[] newPdf = helper.GeneratePdf(cv);
            TempData["bytes"] = newPdf;
            //TempData["url"] = url;

            return View();
        }

        public ActionResult Download()
        {
            byte[] thePdf = (byte[])TempData["bytes"];
            string name = (string)TempData["pdfName"];
            
            return File(thePdf, "application/pdf", name);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}