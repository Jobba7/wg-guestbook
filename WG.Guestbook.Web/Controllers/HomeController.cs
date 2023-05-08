using Microsoft.AspNetCore.Mvc;

namespace WG.Guestbook.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Home/Error/{code:int}")]
        public IActionResult Error(int code)
        {
            return View(code);
        }
    }
}