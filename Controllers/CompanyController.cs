using ResumeStripper.DAL;
using System.Linq;
using System.Web.Mvc;

namespace ResumeStripper.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            StripperContext context = new StripperContext();
            return View(context.Cvs.ToList());
        }
    }
}