using ResumeStripper.DAL;
using ResumeStripper.Helpers;
using ResumeStripper.Models.AccountModels;
using ResumeStripper.Models.AccountModels.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace ResumeStripper.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        protected static StripperContext Context = ContextHelper.GetContext();
        protected readonly CompanyRepository CompanyRepo = new CompanyRepository(Context);

        public CompanyController()
        {
            Context = ContextHelper.GetContext();
        }

        public CompanyController(StripperContext context)
        {
            //constructor for testing
            Context = context;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Register()
        {
            CompanyRegisterViewModel model = null;
            if (TempData["ViewD"] != null)
            {
                //previous model submit attempt was invalid, return to Register page
                ViewData = (ViewDataDictionary)TempData["ViewD"];
                //if there was a RegisterViewModel saved in Tempdata, return Register view with previous inputted values 
                model = (CompanyRegisterViewModel)TempData["regMod"];
            }
            return View(model);
        }
        [Authorize]
        public ActionResult RegisterCompany(CompanyRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var company = CompanyRepo.GetByName(model.Name);

                if (company != null)
                {
                    //company name has already an company in database, generate error and return to Register view
                    ModelState.AddModelError("", "Your chosen company name is already taken!\nMaybe this company already exists in the database.");
                    //save viewdata and return to view
                    TempData["ViewD"] = ViewData;
                    return RedirectToAction("Register");
                }

                //Company does not exist in database if code gets to here

                Company c = new Company()
                {
                    Name = model.Name,
                    Location = model.Location,
                    Sector = model.Sector,
                    Package = model.Package
                };
                try
                {
                    CompanyRepo.Add(c);
                    CompanyRepo.SaveChanges();
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
            return RedirectToAction("EhvPanel", "Home");
        }

        [Authorize(Roles = "")]
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
            int companyId = Convert.ToInt32(id.Replace("Edit", ""));

            TempData["compID"] = companyId;

            Company c = CompanyRepo.GetById(companyId);

            EditCompanyViewModel model = new EditCompanyViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,
                Sector = c.Sector,
                Package = c.Package,
                CurrentUserRole = u.Role
            };

            return View(model);
        }

        [Authorize]
        public ActionResult EditCompany(EditCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    model.Id = (int)TempData["compID"];
                }

                Company c = new Company
                {
                    Id = model.Id,
                    Name = model.Name,
                    Location = model.Location,
                    Sector = model.Sector,
                    Package = model.Package
                };

                CompanyRepo.UpdateCompany(c);
                CompanyRepo.SaveChanges();

                //saves bool in tempdata to force a refresh of context and repositories in panel action,
                //else it will retrieve old data from the database instead of the updated data.
                TempData["UpdateHappened"] = true;
                return RedirectToAction("EhvPanel", "Home");
            }

            TempData["ViewD"] = ViewData;
            return RedirectToAction("Edit", "Company");
        }

        public ActionResult Details(string id)
        {
            //retrieves relevant admin account from tempdata
            User u = (User)TempData["CurrentUser"];
            //and places it back for further use
            TempData["CurrentUser"] = u;

            //gets userId from actionlink ID
            int companyId = Convert.ToInt32(id.Replace("Details", ""));

            Company c = CompanyRepo.GetById(companyId);

            DetailsViewModel model = new DetailsViewModel
            {
                Company = c,
                CurrentUserRole = u.Role
            };

            return View(model);
        }

        public ActionResult Remove(string id)
        {
            //gets userId from actionlink ID
            int companyId = Convert.ToInt32(id.Replace("Remove", ""));

            Company c = CompanyRepo.GetById(companyId);

            if (c.Name.Equals("EHV Talent B.V."))
            {
                //unable to delete EHV Talent B.V. like this, return with error
                const string returnError = "You cannot delete EHV Talent B.V.";
                TempData["CompanyReturnError"] = returnError;
                //return Json(new { success = false, responseText = returnError }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("EhvPanel", "Home");
            }

            try
            {
                CompanyRepo.Delete(c);
                CompanyRepo.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //Something went wrong, return to panel
                TempData["ViewD"] = ViewData;
                return RedirectToAction("EhvPanel", "Home");
            }
            return RedirectToAction("EhvPanel", "Home");
        }
    }
}