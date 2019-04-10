using ResumeStripper.DAL;
using ResumeStripper.Models.AccountModels;
using ResumeStripper.Models.AccountModels.ViewModels;
using ResumeStripper.Models.Enums;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ResumeStripper.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        protected static readonly StripperContext Context = new StripperContext();
        protected readonly UserRepository UserRepo = new UserRepository(Context);
        protected readonly CompanyRepository CompanyRepo = new CompanyRepository(Context);

        //dashboard
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            User u = (User)TempData["DBUser"];

            DashboardViewModel model = new DashboardViewModel()
            {
                Emailaddress = u.Emailaddress,
                Company = u.UserCompany
            };

            return View(model);
        }

        [Authorize] //only for EHV administrators
        public ActionResult EhvPanel()
        {
            //retrieves relevant admin account from tempdata
            //User u = (User)TempData["EAdmin"];
            User u = UserRepo.GetById(1);

            //if (!u.Role.Equals(UserRole.EHVAdmin))
            //{
            //    //TODO: user doesn't have the right Role, return to wherever or show error.
            //}

            //retrieves all users
            List<User> users = UserRepo.GetAll();

            //removes anything to do with password for all users, since they are not needed and are a security concern
            foreach (var us in users)
            {
                us.Password = "";
                us.Salt = "";
            }

            //retrieves all companies
            List<Company> companies = CompanyRepo.GetAll();

            //creates and fills Admin Panel View Model
            EHVAdminPanelViewModel model = new EHVAdminPanelViewModel()
            {
                Email = u.Emailaddress,
                Role = u.Role,
                Companies = companies,
                Users = users
            };

            return View(model);
        }

        [Authorize] //only for Company administrators
        public ActionResult CompanyPanel()
        {
            //retrieves relevant admin account from tempdata
            User u = (User)TempData["CAdmin"];

            if (!u.Role.Equals(UserRole.CompanyAdmin))
            {
                //TODO: user doesn't have the right Role, return to wherever or show error.
            }

            return View();
        }
    }
}