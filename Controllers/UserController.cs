using ResumeStripper.DAL;
using ResumeStripper.Helpers;
using ResumeStripper.Models.AccountModels;
using ResumeStripper.Models.AccountModels.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ResumeStripper.Controllers
{
    public class UserController : Controller
    {
        protected static readonly StripperContext Context = new StripperContext();
        protected readonly UserRepository UserRepo = new UserRepository(Context);
        protected readonly CompanyRepository CompanyRepo = new CompanyRepository(Context);

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
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
                    Companies = CompanyRepo.GetAll()
                };
            }

            return View(model);
        }

        public ActionResult Login()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

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
        public ActionResult RegisterUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Company registering/login to assign Company to any registered users if needed
                //create instance of (Password)Hasher
                var hasher = new Hasher();

                var user = UserRepo.GetUserByEmail(model.Email);

                if (user != null)
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

                //generates salt and hashes password
                var salt = hasher.GenerateSalt();
                var password = hasher.Encrypt(model.Password, salt);

                User u = new User
                {
                    Emailaddress = model.Email,
                    Password = password,
                    Salt = salt,
                    Role = model.Role,
                    UserCompany = model.Company
                };

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
            //TODO: return message that registration was succesful!
            return RedirectToAction("Register");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
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

                        Response.Cookies.Add
                        (
                            new HttpCookie
                            (
                                FormsAuthentication.FormsCookieName,
                                FormsAuthentication.Encrypt(ticket)
                            )
                        );
                        //save user in tempdata for cross-controller transport
                        TempData["DBUser"] = user;

                        //go to dashboard
                        return RedirectToAction("Index", "Home");
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
    }
}