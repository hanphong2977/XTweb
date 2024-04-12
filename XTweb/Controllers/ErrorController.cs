using Microsoft.AspNetCore.Mvc;

namespace XTweb.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult BaoLoi()
        {
            return View();
        }
    }
}
