using ResumeStripper.Models.AccountModels.ViewModels;
using System.Web.Mvc;

namespace ResumeStripper.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            //if there was a registerviewmodel saved in Tempdata, return Register view with previous inputted values 
            var model = (RegisterViewModel)TempData["regMod"];
            return View(model ?? new RegisterViewModel());
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RegisterUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {
                //if modelstate is somehow invalid
                //save model and return to register view
                TempData["regMod"] = model;
                return RedirectToAction("Register");
            }
            return RedirectToAction("Register");
        }


        public ActionResult LoginUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

            }

            return null;
        }
    }
}
