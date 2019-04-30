using ResumeStripper.DAL;
using ResumeStripper.Helpers;
using ResumeStripper.Managers;
using ResumeStripper.Models.AccountModels;
using ResumeStripper.Models.AccountModels.ViewModels;
using ResumeStripper.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ResumeStripper.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        protected IUserRepository UserRepo;
        protected ICompanyRepository CompanyRepo;

        public UserController()
        {
            StripperContext context = ContextHelper.GetContext();
            UserRepo = new UserRepository(context);
            CompanyRepo = new CompanyRepository(context);
        }

        public UserController(StripperContext context, ICompanyRepository rep)
        {
            //for testing
            UserRepo = new UserRepository(context);
            CompanyRepo = rep;
        }

        public UserController(StripperContext context, IUserRepository rep)
        {
            //for testing
            CompanyRepo = new CompanyRepository(context);
            UserRepo = rep;
        }

        public UserController(IUserRepository urep, ICompanyRepository crep)
        {
            //for testing
            CompanyRepo = crep;
            UserRepo = urep;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult AccountProfile()
        {
            return View();
        }

        [Authorize]
        public ActionResult Register()
        {
            //retrieves relevant admin account from tempdata
            User u = (User)TempData["CurrentUser"];
            //and places it back for further use
            TempData["CurrentUser"] = u;

            RegisterViewModel model = null;
            if (TempData["ViewD"] != null)
            {
                //previous model submit attempt was invalid, return to Register page
                ViewData = (ViewDataDictionary)TempData["ViewD"];
                //if there was a RegisterViewModel saved in Tempdata, return Register view with previous inputted values 
                model = (RegisterViewModel)TempData["regMod"];
            }

            if (model == null)
            {
                model = new RegisterViewModel
                {
                    Companies = GetAllCompanies()
                };
            }
            else if (model.Companies == null)
            {
                model.Companies = GetAllCompanies();
            }

            if (u != null)
            {
                model.CurrentUserRole = u.Role;
                model.CurrentCompanyName = u.UserCompany.Name;
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            HttpCookie cookie = null;

            //the if not null is for testing purposes
            if (Request.Cookies != null)
            {
                cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            }

            if (cookie != null)
            {
                var ticketInfo = FormsAuthentication.Decrypt(cookie.Value);
                if (ticketInfo != null)
                {
                    //there was a 'Remember Me' Cookie, so retrieve User and do login
                    User cookieUser = UserRepo.GetById(Convert.ToInt32(ticketInfo.Name));

                    //save user in tempdata for cross-controller transport
                    TempData["DBUser"] = cookieUser;

                    //go to dashboard
                    return RedirectToAction("Index", "Home");
                }
            }

            LoginViewModel model = null;
            if (TempData["ViewD"] != null)
            {
                //previous model submit attempt was invalid, return to Login page
                ViewData = (ViewDataDictionary)TempData["ViewD"];
                //if there was a LoginViewModel saved in Tempdata, return Login view with previous inputted values 
                model = (LoginViewModel)TempData["regMod"];
            }
            return View(model ?? new LoginViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public ActionResult RegisterUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //create instance of (Password)Hasher
                var hasher = new Hasher();

                //var user = UserRepo.GetUserByEmail(model.Email);
                UserManager uManager = new UserManager(UserRepo);

                //check if user exists
                //if (user != null)
                if (uManager.CheckIfUserExists(model.Email))
                {
                    //email has already an user in database, generate error and return to Register view
                    ModelState.AddModelError("", "Your chosen emailaddress is already taken!");
                    //save viewdata and return to view
                    TempData["ViewD"] = ViewData;
                    return RedirectToAction("Register"); //TODO return to dashboard for example
                }

                //user does not exist in database if code gets to here

                //retrieve full company information
                model.Company = CompanyRepo.GetByName(model.Company.Name);

                //fills the new user, encrypts password etc
                User u = uManager.FillUser(model);

                try
                {
                    UserRepo.Add(u);
                    UserRepo.SaveChanges();
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

                    //Something went wrong, consider Modelstate invalid and return values to Register View
                    //save model and return to register view
                    TempData["regMod"] = model;
                    TempData["ViewD"] = ViewData;
                    return RedirectToAction("Register");
                }
            }
            else
            {
                //if modelstate is somehow invalid
                //save model and return to register view
                TempData["regMod"] = model;
                TempData["ViewD"] = ViewData;
                return RedirectToAction("Register");
            }

            //retrieves relevant admin account from tempdata
            User cu = (User)TempData["CurrentUser"];
            //and places it back for further use
            TempData["CurrentUser"] = cu;

            if (cu != null)
            {
                switch (cu.Role)
                {
                    case UserRole.EHVAdmin:
                        return RedirectToAction("EhvPanel", "Home");
                    case UserRole.CompanyAdmin:
                        return RedirectToAction("CompanyPanel", "Home");
                }
            }

            return RedirectToAction("Register", "User");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginUser(LoginViewModel model)
        {
            const string mainError = "You have entered an invalid emailaddress or password!";
            if (ModelState.IsValid)
            {
                //create instance of (Password)Hasher
                var hasher = new Hasher();

                var user = UserRepo.GetUserByEmail(model.Email);

                if (user != null)
                {
                    //user with email exists in database, check password
                    if (hasher.VerifyPassword(model.Password, user.Password, user.Salt))
                    {
                        //password matches stored password, so login
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                            1,
                            user.ID.ToString(),
                            DateTime.Now,
                            DateTime.Now.AddHours(2),
                            model.IsRemember,
                            "",
                            FormsAuthentication.FormsCookiePath);

                        //make cookie and for XSRF Protection put HttpOnly on true
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
                        {
                            HttpOnly = true
                        };

                        Response.Cookies.Add(cookie);

                        //save user in tempdata for cross-controller transport
                        TempData["CurrentUser"] = user;

                        //go to dashboard
                        return RedirectToAction("Index", "Cv");
                    }
                    //password doesn't match stored password, add error and return login view
                    ModelState.AddModelError("", mainError);
                    //save viewdata and return to view
                    TempData["ViewD"] = ViewData;
                    return RedirectToAction("Login");
                }
                //Input Emailaddress doesn't exist, give error and return to Login view
                ModelState.AddModelError("", mainError);
                //save viewdata and return to view
                TempData["ViewD"] = ViewData;
                return RedirectToAction("Login");
            }
            //if modelstate is somehow invalid
            //save model and return to register view
            TempData["regMod"] = model;
            TempData["ViewD"] = ViewData;
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult LogOut()
        {
            //remove current session
            Session.Clear();
            Session.Abandon();

            //clear all cookies associated with the site
            string[] myCookies = Request.Cookies.AllKeys;
            if (Response.Cookies != null)
            {
                foreach (string cookie in myCookies)
                {
                    if (!string.IsNullOrEmpty(cookie))
                    {
                        Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                    }
                }
            }
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult Edit(string id)
        {
            if (TempData["ViewD"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewD"];
                return View();
            }

            //retrieves relevant admin account from tempdata
            User u = (User)TempData["CurrentUser"];
            //and places it back for further use
            TempData["CurrentUser"] = u;

            //gets userId from actionlink ID
            int userId = Convert.ToInt32(id.Replace("Edit", ""));

            TempData["userID"] = userId;

            User user = UserRepo.GetById(userId);

            UserRole current;

            if (u == null)
            {
                //for testing
                current = UserRole.CompanyUser;
            }
            else
            {
                current = u.Role;
            }

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.ID,
                Emailaddress = user.Emailaddress,
                Role = user.Role,
                UserCompany = user.UserCompany,
                Companies = GetAllCompanies(),
                CurrentUserRole = current
            };

            return View(model);
        }

        [Authorize]
        public ActionResult EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserCompany = CompanyRepo.GetByName(model.UserCompany.Name);

                if (model.Id == 0)
                {
                    model.Id = (int)TempData["userID"];
                }

                User newUser = UserRepo.GetUserByEmail(model.Emailaddress);

                newUser.UserCompany = model.UserCompany;
                newUser.Role = model.Role;

                UserRepo.Update(newUser);

                //saves bool in tempdata to force a refresh of context and repositories in panel action,
                //else it will retrieve old data from the database instead of the updated data.
                TempData["UpdateHappened"] = true;

                //retrieves relevant admin account from tempdata
                User cu = (User)TempData["CurrentUser"];
                //and places it back for further use
                TempData["CurrentUser"] = cu;

                if (cu != null)
                {
                    switch (cu.Role)
                    {
                        case UserRole.EHVAdmin:
                            return RedirectToAction("EhvPanel", "Home");
                        case UserRole.CompanyAdmin:
                            return RedirectToAction("CompanyPanel", "Home");
                    }
                }
            }
            TempData["ViewD"] = ViewData;
            return RedirectToAction("Edit", "User");
        }

        [Authorize]
        public ActionResult Details(string id)
        {
            //retrieves relevant admin account from tempdata
            User u = (User)TempData["CurrentUser"];
            //and places it back for further use
            TempData["CurrentUser"] = u;

            //gets userId from actionlink ID
            int userId = Convert.ToInt32(id.Replace("Details", ""));

            User user = UserRepo.GetById(userId);

            UserRole current;

            if (u == null)
            {
                //for testing
                current = UserRole.CompanyUser;
            }
            else
            {
                current = u.Role;
            }

            DetailsViewModel model = new DetailsViewModel
            {
                Emailaddress = user.Emailaddress,
                Role = user.Role,
                Company = user.UserCompany,
                CurrentUserRole = current
            };

            return View(model);
        }

        public ActionResult Remove(string id)
        {
            //retrieves relevant admin account from tempdata
            User cu = (User)TempData["CurrentUser"];
            //and places it back for further use
            TempData["CurrentUser"] = cu;

            //gets userId from actionlink ID
            int userId = Convert.ToInt32(id.ToString().Replace("Remove", ""));

            User u = UserRepo.GetById(userId);

            if (u.Role == UserRole.EHVAdmin)
            {
                //unable to delete EHV admin like this, return with error
                const string returnError = "You cannot delete an EHV Administrator.";
                TempData["UserReturnError"] = returnError;
                //return Json(new {success = false, responseText = returnError}, JsonRequestBehavior.AllowGet);
                return RedirectToAction("EhvPanel", "Home");
            }

            try
            {
                UserRepo.Delete(u);
                UserRepo.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //Something went wrong, return to panel
                TempData["ViewD"] = ViewData;
                switch (cu.Role)
                {
                    case UserRole.EHVAdmin:
                        return RedirectToAction("EhvPanel", "Home");
                    case UserRole.CompanyAdmin:
                        return RedirectToAction("CompanyPanel", "Home");
                }
            }

            if (cu == null)
            {
                //for testing
                return RedirectToAction("Index", "CV");
            }

            switch (cu.Role)
            {
                case UserRole.EHVAdmin:
                    return RedirectToAction("EhvPanel", "Home");
                case UserRole.CompanyAdmin:
                    return RedirectToAction("CompanyPanel", "Home");
            }
            //shouldnt ever get here tbh
            return null;
        }

        public List<Company> GetAllCompanies()
        {
            return CompanyRepo.GetAll();
        }

        public List<User> GetAllUsers()
        {
            return UserRepo.GetAll();
        }

        public List<User> GetAllUsersOfCompany(int id)
        {
            return UserRepo.GetAllByCompanyId(id);
        }
    }
}