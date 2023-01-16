using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blogovic.Filters
{
    public class LoggedUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userId = context.HttpContext.Session.GetString("userId");
            string eMail = context.HttpContext.Session.GetString("email");

            if (string.IsNullOrEmpty(userId))
            {
                string routePath = context.HttpContext.Request.Path;

                context.Result = new RedirectToActionResult("Login", "Auth", new { yonlen = routePath });
            }
            if (string.IsNullOrEmpty(eMail))
            {
                string routePath = context.HttpContext.Request.Path;

                context.Result = new RedirectToActionResult("Login", "Auth", new { yonlen = routePath });
            }
        }
    }
}
