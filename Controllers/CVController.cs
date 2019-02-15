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

namespace ResumeStripper.Controllers
{
    public class CVController : Controller
    {
        private StripperContext db = new StripperContext();

        [HttpGet]
        public ActionResult Index()
        {
            MessageViewModel model = (MessageViewModel)TempData["text"];
            if (model != null)
            {
                //ViewData["UploadedPdf"] = model;
                return View(model);
            }
            else
            {
                return View();
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
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    //var filename = path.getfilename(file.filename);
                    //var path = path.combine(server.mappath("~/app_data/uploads"), filename);
                    //file.saveas(path);
                    PDFHelper helper = new PDFHelper();
                    MessageViewModel mod = new MessageViewModel
                    {
                        //Text = helper.getTextFromPdf(file)
                        Text = file.FileName
                    };
                    TempData["text"] = mod;
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: CV/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = db.CVS.Find(id);
            if (cV == null)
            {
                return HttpNotFound();
            }
            return View(cV);
        }

        // GET: CV/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CV/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Prefix,Surname,Residence,Country,DateOfBirth,Profile")] CV cV)
        {
            if (ModelState.IsValid)
            {
                db.CVS.Add(cV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cV);
        }

        // GET: CV/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = db.CVS.Find(id);
            if (cV == null)
            {
                return HttpNotFound();
            }
            return View(cV);
        }

        // POST: CV/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Prefix,Surname,Residence,Country,DateOfBirth,Profile")] CV cV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cV);
        }

        // GET: CV/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CV cV = db.CVS.Find(id);
            if (cV == null)
            {
                return HttpNotFound();
            }
            return View(cV);
        }

        // POST: CV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CV cV = db.CVS.Find(id);
            db.CVS.Remove(cV);
            db.SaveChanges();
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