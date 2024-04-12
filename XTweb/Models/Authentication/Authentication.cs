using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace XTweb.Models.Authentication
{
    public class Authentication: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("sdt") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary()
                    {
                        {"Controller","User"},
                        {"Action","dangnhap" }
                    });
            }
        }

    }
}
