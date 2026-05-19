using Microsoft.AspNetCore.Mvc;
using ShopSample.ViewModels;
using System.Text.Json;

namespace ShopSample.Controllers
{
    public class RegisterController : Controller
    {
        // 入力画面（GET）
        [HttpGet]
        public IActionResult Index()
        {
            return View(new RegisterUserInputModel());
        }

        // 入力画面（POST）→ 確認画面へリダイレクト（PRG）
        [HttpPost]
        public IActionResult Index(RegisterUserInputModel model)
        {
            // TempData に保存して確認画面へ
            TempData["RegisterData"] = JsonSerializer.Serialize(model);

            return RedirectToAction("Confirm");
        }

        // 確認画面（GET）
        [HttpGet]
        public IActionResult Confirm()
        {
            if (!TempData.ContainsKey("RegisterData"))
            {
                return RedirectToAction("Index");
            }

            var model = JsonSerializer.Deserialize<RegisterUserInputModel>(
                TempData["RegisterData"]!.ToString()!
            );

            // Confirm → Complete のために再度 TempData に入れ直す
            TempData["RegisterData"] = JsonSerializer.Serialize(model);

            return View(model);
        }

        // 確認画面（POST）→ 完了画面へリダイレクト（PRG）
        [HttpPost]
        public IActionResult ConfirmPost()
        {
            // 完了画面で表示するためのデータを取得
            if (!TempData.ContainsKey("RegisterData"))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Complete");
        }

        // 完了画面（GET）
        [HttpGet]
        public IActionResult Complete()
        {
            if (!TempData.ContainsKey("RegisterData"))
            {
                return RedirectToAction("Index");
            }

            var model = JsonSerializer.Deserialize<RegisterUserInputModel>(
                TempData["RegisterData"]!.ToString()!
            );

            return View(model);
        }
    }
}
