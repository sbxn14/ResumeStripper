using ResumeStripper.Filters;
using System.Web.Mvc;

namespace ResumeStripper
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}