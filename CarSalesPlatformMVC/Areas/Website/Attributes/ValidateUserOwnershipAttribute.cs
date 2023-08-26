namespace CarSalesPlatformMVC.Areas.Website.Attributes
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Security.Claims;

    public class ValidateUserOwnershipAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            // Eğer kullanıcı admin ise kontrolü geç
            if (user.IsInRole("Admin"))
            {
                return;
            }
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            foreach (var argument in context.ActionArguments.Values)
            {
                var property = argument.GetType().GetProperty("UserId");
                if (property != null)
                {
                    var requestId = property.GetValue(argument).ToString();
                    if (userId != requestId)
                    {
                        context.Result = new UnauthorizedObjectResult("Bu işlemi gerçekleştirme yetkiniz yok.");
                        return; // Eğer eşleşmeyen bir userId bulduysak, daha fazla kontrol yapmamıza gerek yok.
                    }
                }
            }
        }
    }

}
