using System.Web;
using System.Web.Mvc;

namespace havenix.Filters
{
    public class LoginAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;

            if (session["UserId"] == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}