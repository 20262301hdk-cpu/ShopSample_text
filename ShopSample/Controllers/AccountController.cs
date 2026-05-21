using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopSample.ViewModels;
using System.Diagnostics;

namespace ShopSample.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "ログインに失敗しました。");
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
        public IActionResult EditUser()
        {
            // 初期表示時は固定値
            UserInfometionViewModel model = new UserInfometionViewModel
            {
                UserName = "山田太郎",
                Age = 28,
                IsAdmin = true
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditUser(UserInfometionViewModel model)
        {
            // 本来は DB 更新するが、今回は擬似的に成功メッセージを表示
            ViewData["Message"] = "ユーザー情報を更新しました。";
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

