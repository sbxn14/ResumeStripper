using ResumeStripper.DAL;
using ResumeStripper.Helpers;
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
        protected IUserRepository UserRepo;
        protected ICompanyRepository CompanyRepo;

        private readonly bool _isTesting = false;

        public HomeController()
        {
            StripperContext Context = ContextHelper.GetContext();
            UserRepo = new UserRepository(Context);
            CompanyRepo = new CompanyRepository(Context);
        }

        public HomeController(StripperContext context)
        {
            //for testing
            UserRepo = new UserRepository(context);
            CompanyRepo = new CompanyRepository(context);
            _isTesting = true;
        }

        public HomeController(IUserRepository uRep, ICompanyRepository cRep)
        {
            //for testing
            UserRepo = uRep;
            CompanyRepo = cRep;
            _isTesting = true;
        }

        public HomeController(StripperContext context, IUserRepository uRep)
        {
            //for testing
            UserRepo = uRep;
            CompanyRepo = new CompanyRepository(context);
            _isTesting = true;
        }

        public HomeController(StripperContext context, ICompanyRepository cRep)
        {
            //for testing
            UserRepo = new UserRepository(context);
            CompanyRepo = cRep;
            _isTesting = true;
        }

        //dashboard
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            //gets user out of tempdata
            User u = (User)TempData["CurrentUser"];
            //places it back in tempdata for further use
            TempData["CurrentUser"] = u;

            if (u == null)
            {
                //for testing
                u = new User
                {
                    Emailaddress = "Testmail@mail.com",
                    UserCompany = new Company
                    {
                        Name = "TestCompany"
                    }
                };
            }

            DashboardViewModel model = new DashboardViewModel()
            {
                Emailaddress = u.Emailaddress,
                Company = u.UserCompany
            };

            return View(model ?? new DashboardViewModel());
        }

        [Authorize] //only for EHV administrators
        public ActionResult EhvPanel()
        {
            //retrieves relevant admin account from tempdata
            User u = (User)TempData["CurrentUser"];
            //and places it back for further use
            TempData["CurrentUser"] = u;

            if (u == null) u = new User { Role = UserRole.EHVAdmin };

            //checks if user is allowed to view EHVpanel
            if (u.Role != UserRole.EHVAdmin)
            {
                if (u.Role == UserRole.CompanyAdmin)
                {
                    //bring to Company panel
                    return RedirectToAction("CompanyPanel", "Home");
                }
                //redirect to previous page
                if (Request.UrlReferrer != null)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
                //else just return to the stripper
                return RedirectToAction("Index", "Cv");
            }

            //if not testing
            if (!_isTesting)
            {
                //checks based on a saved TempData bool if context and repositories need to be refreshed/recreated
                if (TempData["UpdateHappened"] != null)
                {
                    if ((bool)TempData["UpdateHappened"])
                    {
                        StripperContext context = new StripperContext();
                        UserRepo = new UserRepository(context);
                        CompanyRepo = new CompanyRepository(context);
                    }
                }
            }

            //retrieves all companies
            List<Company> companies = CompanyRepo.GetAll();
            //retrieves all users
            List<User> users = UserRepo.GetAll();

            //for each company, get all users and save them in company entity for usercount in view
            foreach (Company c in companies)
            {
                c.Users = UserRepo.GetAllByCompanyId(c.Id);
            }

            //removes anything to do with password for all users, since they are not needed and are a security concern
            foreach (var us in users)
            {
                us.Password = "";
                us.Salt = "";
            }

            //creates and fills Admin Panel View Model
            EHVAdminPanelViewModel model = new EHVAdminPanelViewModel()
            {
                Email = u.Emailaddress,
                Role = u.Role,
                Companies = companies,
                Users = users,
                UserReturnError = (string)TempData["UserReturnError"],
                CompanyReturnError = (string)TempData["CompanyReturnError"]
            };

            TempData["vModel"] = model;

            return View(model);
        }

        [Authorize] //only for Company administrators
        public ActionResult CompanyPanel()
        {
            //retrieves relevant admin account from tempdata
            User u = (User)TempData["CurrentUser"];
            //and places it back for further use
            TempData["CurrentUser"] = u;

            if (_isTesting)
            {
                //for testing
                if (u == null)
                {
                    //fills u with a random user with test  values
                    u = new User(1, "test@test.com", "password", "salt", UserRole.CompanyAdmin, new Company
                        (1, "testName", "testLocation", "testSector", StripperPackage.B));
                }
            }

            //checks if user is allowed to view Companypanel
            if (u.Role != UserRole.CompanyAdmin)
            {
                if (u.Role == UserRole.EHVAdmin)
                {
                    //bring to EHV panel
                    return RedirectToAction("EhvPanel", "Home");
                }
                //redirect to previous page
                if (Request.UrlReferrer != null)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
                //else just return to the stripper
                return RedirectToAction("Index", "Cv");
            }

            //if not testing
            if (!_isTesting)
            {
                //checks based on a saved TempData bool if context and repositories need to be refreshed/recreated
                if (TempData["UpdateHappened"] != null)
                {
                    if ((bool)TempData["UpdateHappened"])
                    {
                        StripperContext context = new StripperContext();
                        UserRepo = new UserRepository(context);
                        CompanyRepo = new CompanyRepository(context);
                    }
                }
            }

            //retrieves all users of the company
            List<User> companyUsers = UserRepo.GetAllByCompanyId(u.UserCompany.Id);

            //removes anything to do with password for all users, since they are not needed and are a security concern
            if (companyUsers == null) return View();

            foreach (var us in companyUsers)
            {
                us.Password = "";
                us.Salt = "";
            }

            //creates and fills Admin Panel View Model
            CompanyAdminPanelViewModel model = new CompanyAdminPanelViewModel
            {
                Company = u.UserCompany,
                Email = u.Emailaddress,
                UserCount = companyUsers.Count,
                TotalAllowedUsers = u.UserCompany.GetPackageUserCount(),
                Role = u.Role
            };

            model.Company.Users = companyUsers;

            TempData["vModel"] = model;

            return View(model);
        }
    }
}