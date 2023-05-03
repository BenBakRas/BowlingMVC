using Microsoft.AspNetCore.Mvc;

namespace BowlingMVC.Controllers
{
    public class BookingController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
