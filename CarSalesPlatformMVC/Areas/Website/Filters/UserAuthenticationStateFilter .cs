using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarSalesPlatformMVC.Areas.Website.Filters
{
    public class UserAuthenticationStateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool userIsAuthenticated = context.HttpContext.Items["UserIsAuthenticated"] != null &&
                                       (bool)context.HttpContext.Items["UserIsAuthenticated"];

            var controller = context.Controller as Controller;
            if (controller != null)
            {
                controller.ViewBag.UserIsAuthenticated = userIsAuthenticated;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }

}
