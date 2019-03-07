using ResumeStripper.DAL;
using ResumeStripper.Helpers;
using ResumeStripper.Models;
using ResumeStripper.Models.Viewmodels;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System;

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
                string filename = (string)TempData["filename"];

                if (filename.Contains(":"))
                {
                    //edge filename
                    filename = Path.GetFileName(filename);
                }

                model.serverPath = "http://192.168.86.29:8084/" + filename;
                ViewBag.JavaScriptFunction = "newPDFArrived('" + model.serverPath + "');";
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
                        //temporarily saves file
                        //var filePath = Path.Combine(Server.MapPath("~/Content/pdf"), pdf.FileName);
                        TempData["filename"] = pdf.FileName;
                        //pdf.SaveAs(filePath);

                        PDFHelper helper = new PDFHelper();

                        MessageViewModel mod = new MessageViewModel
                        {
                            //Text = helper.GetHTMLText(filePath),
                            Path = pdf.FileName
                        };
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
        public ActionResult Export(MessageViewModel model)
        {
            if (model.ResultCv != null)
            {
                CV cv = model.ResultCv;
                db.CVS.Add(cv);
                db.SaveChanges();

                //generate PDF and generate view that shows that PDF instead of redirect to index
                string url = Server.MapPath("~/PDFs/") + "CVTest.pdf";

                PDFHelper helper = new PDFHelper();

                helper.GetPDF(url, cv);
            }
            return RedirectToAction("Index");
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