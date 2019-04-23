using ResumeStripper.Models.Enums;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeStripper.Attributes
{
    public class CustomRoleAuthorizeAttribute : AuthorizeAttribute
    {
        public UserRole[] AuthRoles { get; set; }

        public CustomRoleAuthorizeAttribute(params UserRole[] roles) : base()
        {
            AuthRoles = roles;
            Roles = string.Join(",", Enum.GetNames(typeof(UserRole)));
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);

            if (!isAuthorized)
            {
                return false;
            }
            
            
        }
    }
}