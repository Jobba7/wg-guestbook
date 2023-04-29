using Microsoft.AspNetCore.Mvc;

namespace WG.Guestbook.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}