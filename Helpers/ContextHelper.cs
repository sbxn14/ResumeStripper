using ResumeStripper.DAL;
using System.Web;

namespace ResumeStripper.Helpers
{
    public static class ContextHelper
    {
        private const string ContextKey = "StripperContext";

        public static StripperContext GetContext()
        {
            if (HttpContext.Current.Items[ContextKey] == null)
            {
                HttpContext.Current.Items.Add(ContextKey, new StripperContext());
            }

            return (StripperContext)HttpContext.Current.Items[ContextKey];
        }

        public static void DisposeContext()
        {
            if (HttpContext.Current.Items[ContextKey] != null)
            {
                var context = (StripperContext)HttpContext.Current.Items[ContextKey];
                context.Dispose();
            }
        }
    }
}