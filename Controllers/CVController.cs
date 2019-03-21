using ResumeStripper.DAL;
using ResumeStripper.Helpers;
using ResumeStripper.Models;
using ResumeStripper.Models.Enums;
using ResumeStripper.Models.Viewmodels;
using System.Data.Entity.Validation;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ResumeStripper.Controllers
{
    public class CVController : Controller
    {
        private StripperContext db = new StripperContext();

        [HttpGet]
        public ActionResult Index()
        {
            MessageViewModel model = (MessageViewModel)TempData["Message"];
            if (model != null)
            {
                string filename = (string)TempData["file"];

                if (filename.Contains(":"))
                {
                    //edge filename
                    filename = Path.GetFileName(filename);
                }

                model.ServerPath = "http://192.168.86.26:8081/" + filename;
                ViewBag.JavaScriptFunction = "newPDFArrived('" + model.ServerPath + "');";
                return View(model);
            }
            else
            {
                return View(new MessageViewModel());
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase pdf)
        {
            //select file button has been pressed
            //if (Request.Files.Count > 0)
            if (pdf.ContentLength > 0)
            {
                string extension = Path.GetExtension(pdf.FileName).ToUpper();
                //checks if file is pdf, if not will give error
                if (extension.Equals(".PDF"))
                {
                    if (pdf != null && pdf.ContentLength > 0)
                    {
                        TempData["file"] = pdf.FileName;

                        MessageViewModel mod = new MessageViewModel
                        {
                            Path = pdf.FileName
                        };

                        //TODO CALL EXTRACTION METHOD

                        TempData["Message"] = mod;
                    }
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
            if (model.ResultCv != null)
            {
                CV cv = model.ResultCv;
                //removes any accidental whitespaces at beginning and end of every value in the CV
                cv.TrimEverything();

                foreach (Language l in cv.Languages)
                {
                    if (l.LevelOfListening.Equals(LanguageLevel.Basic) && l.LevelOfSpeaking.Equals(LanguageLevel.Basic) && l.LevelOfWriting.Equals(LanguageLevel.Basic))
                    {
                        //probably using simple mode
                        if (!l.Level.Equals(LanguageLevel.Basic))
                        {
                            //simple mode was used
                            l.isSimple = true;
                        }
                        else
                        {
                            l.isSimple = false;
                        }
                    }
                    else
                    {
                        l.isSimple = false;
                    }
                }

                try
                {
                    db.CVS.Add(cv);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

                if (submitter.Equals("Generate Anonymous CV"))
                {
                    //requires anonymous PDF
                    cv.setAnonymousCV();
                }

                string resultName = "";

                if (cv.IsAnonymous)
                {
                    //cv is anonymous
                    resultName = "CV_User_" + cv.ID + "_EHV.pdf";
                }
                else
                {
                    if (cv.Prefix == "") resultName = "CV_" + cv.Name + "_" + cv.Surname + "_EHV.pdf";
                    else resultName = "CV_" + cv.Name + "_" + cv.Prefix + "_" + cv.Surname + "_EHV.pdf";
                }

                //generate PDF and generate view that shows that PDF instead of redirect to index
                string url = Server.MapPath("~/PDFs/") + resultName;

                PDFHelper helper = new PDFHelper();

                byte[] newPdf = helper.GeneratePDF(url, cv);
                TempData["bytes"] = newPdf;
                return RedirectToAction("Download");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Download()
        {
            byte[] thePdf = (byte[])TempData["bytes"];
            return File(thePdf, "application/pdf");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}