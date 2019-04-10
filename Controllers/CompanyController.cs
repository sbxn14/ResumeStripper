using ResumeStripper.DAL;
using ResumeStripper.Models.AccountModels;
using ResumeStripper.Models.AccountModels.ViewModels;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace ResumeStripper.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        protected static readonly StripperContext Context = new StripperContext();
        protected readonly CompanyRepository CompanyRepo = new CompanyRepository(Context);

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
            return RedirectToAction("Register");
        }

        [Authorize]
        public ActionResult DeleteCompany(string name)
        {
            //TODO: check over this and see if it works
            Company model = CompanyRepo.GetByName(name);

            try
            {
                CompanyRepo.Delete(model);
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
                TempData["ViewD"] = ViewData;
                //TODO: return to ehv panel
                return RedirectToAction("Register");
            }

            return RedirectToAction("EhvPanel", "Home");
        }
    }
}