using Microsoft.AspNetCore.Mvc;

namespace VizBill.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
