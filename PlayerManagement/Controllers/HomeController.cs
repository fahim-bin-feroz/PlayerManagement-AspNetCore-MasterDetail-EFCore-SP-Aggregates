using Microsoft.AspNetCore.Mvc;

namespace PlayerManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
