using Microsoft.AspNetCore.Mvc;

namespace ShopSample.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            ViewData["Message"] = "ログイン画面を表示しています。";
            return View();
        }
    }
}
