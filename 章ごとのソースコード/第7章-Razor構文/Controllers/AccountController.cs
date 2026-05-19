using Microsoft.AspNetCore.Mvc;
using ShopSample.ViewModels;
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
        public IActionResult MyPage()
        {
            // ViewModel を作成して値をセット
            var model = new UserInfometionViewModel
            {
                UserName = "山田太郎",
                Age = 28,
                IsAdmin = true,
                PurchaseHistory = new List<PurchaseItem>
                {
                    new PurchaseItem { ProductName = "ノートPC", Price = 120000 },
                    new PurchaseItem { ProductName = "マウス", Price = 2000 },
                    new PurchaseItem { ProductName = "キーボード", Price = 5000 }
                }
            };
            return View(model);
        }
    }
}

