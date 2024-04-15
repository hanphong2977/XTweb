using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using XTweb.Extensions;

namespace XTweb.Models.Authentication
{
    public class Authentication_Cart : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var tmp = context.HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (tmp == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary()
                    {
                        {"Controller","GioHang"},
                        {"Action","Index" }
                    });
            }

        }
    }
}
