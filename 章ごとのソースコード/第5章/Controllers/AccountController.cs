using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            ViewData["Message"] = "IDとパスワードを入力してください。";
            return View();
        }
        public IActionResult Logout()
        {
            return RedirectToAction();
        }
        [HttpPost]
        public IActionResult Login(string userId)
        {
            Debug.WriteLine($"Login:userId:{userId}");
            return RedirectToAction("LoginResult");
        }
        public IActionResult LoginResult(string userId)
        {
            ViewData["Message"] = $"ログインに成功しました。";
            return View();
        }
        public IActionResult SearchUserId(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return View();
            }
            string message = string.Empty;
            if (userId == "123")
            {
                message = $"ユーザーID：123 を検索しました。";
            }
            else if (userId == "456")
            {
                message = $"ユーザーID：456 を検索しました。";
            }
            else
            {
                message = $"ユーザーID：{userId}は存在しません。";
            }
            ViewData["Message"] = message;
            return View();
        }
    }
}
