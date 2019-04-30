using ResumeStripper.DAL;
using ResumeStripper.Filters;
using ResumeStripper.Helpers;
using ResumeStripper.Models;
using ResumeStripper.Models.AccountModels;
using ResumeStripper.Models.Viewmodels;
using System.Data.Entity.Validation;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ResumeStripper.Controllers
{
    [Authorize]
    public class CvController : Controller
    {
        protected ICvRepository Repo;

        public CvController()
        {
            Repo = new CvRepository(ContextHelper.GetContext());
        }

        public CvController(StripperContext context)
        {
            //for testing
            Repo = new CvRepository(context);
        }

        public CvController(ICvRepository repo)
        {
            //for testing
            Repo = repo;
        }

        [HttpGet]
        [WhitespaceFilter]
        [CompressFilter]
        [Authorize]
        public ActionResult Index()
        {
            User u = null;

            if (TempData["CurrentUser"] != null)
            {
                //set user from tempdata
                u = (User)TempData["CurrentUser"];
                //save user again in tempdata for further use
                TempData["CurrentUser"] = u;
            }

            if (TempData["ViewD"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewD"];

                if (!string.IsNullOrEmpty((string)TempData["CurrentURL"]))
                {
                    //if there is a current url saved, reopen PDF
                    ViewBag.JavaScriptFunction = "newPDFArrived('" + (string)TempData["CurrentURL"] + "');";
                }

                StripperViewModel m = (StripperViewModel)TempData["exportForm"];
                m.CurrentUser = u;
                return View(m);
            }

            StripperViewModel model = (StripperViewModel)TempData["Message"];

            //if model is null somehow, return to the index view but keep user
            if (model == null)
            {
                StripperViewModel m = new StripperViewModel { CurrentUser = u };
                TempData["CurrentUser"] = u;
                return View(m);
            }

            string filename = (string)TempData["file"];

            if (filename.Contains(":"))
            {
                //Makes filename Microsoft Edge compatible if needed
                filename = Path.GetFileName(filename);
            }

            model.CurrentUser = u;
            //saves user in tempdata for further use cross-controller
            TempData["CurrentUser"] = u;
            //TODO: een betere manier voor filehosting en al dan de manier die nu gebruikt wordt met http-server npm..
            model.ServerPath = "http://127.0.0.1:8081/" + filename;
            TempData["CurrentURL"] = model.ServerPath;
            ViewBag.JavaScriptFunction = "newPDFArrived('" + model.ServerPath + "');";
            return View(model);
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

                    StripperViewModel mod = new StripperViewModel
                    {
                        Path = pdf.FileName
                    };

                    TempData["Message"] = mod;
                }
                else
                {
                    //TODO: Technically not needed, can be removed probably
                    ViewBag.FileStatus = "Invalid File Format.";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public ActionResult Export(StripperViewModel model, string submitter)
        {
            if (!ModelState.IsValid)
            {
                //validation failed
                TempData["exportForm"] = model;
                TempData["ViewD"] = ViewData;
                return RedirectToAction("Index");
            }
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
                Repo.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
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
                //context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}