using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;

namespace XTweb.Models.Authentication
{
    public class Authentication_Admin: ActionFilterAttribute
    {
        public int IdChucNang {  get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        { 
           
           if (context.HttpContext.Session.GetString("sdt") == null)
            {
                var returnUrl = context.HttpContext.Request.GetDisplayUrl();
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary()
                    {
                        {"Controller","Admin"},
                        {"Action","dangnhap" },
                      
                    });
            }

        }
    }
}
