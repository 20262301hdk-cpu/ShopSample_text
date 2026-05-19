using Microsoft.AspNetCore.Mvc;
using ShopSample.ViewModels;
using System.Text.Json;

namespace ShopSample.Controllers;

public class RegisterController : Controller
{
    // ==========================================
    // 1. 入力画面（GET）
    // ==========================================
    [HttpGet]
    public IActionResult Index()
    {
        var data = TempData["RegisterData"];
        // dataがnullではない=戻るボタンで戻ってきた場合
        if (data != null)
        {
            var model = JsonSerializer.Deserialize<RegisterUserInputModel>(data.ToString());
            // セキュリティのため、パスワードはリセット（消去）する
            model.Password = null;
            model.ConfirmPassword = null;
            return View(model);
        }
        return View(new RegisterUserInputModel());
    }

    // ==========================================
    // 2. 入力画面（POST）→ 確認画面へ
    // ==========================================
    [HttpPost]
    public IActionResult Index(RegisterUserInputModel model)
    {
        // バリデーション結果がFalseなら入力画面に戻す
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        // ModelOnlyを出力するためのフラグ
        // 仮に「メールアドレスがすでに登録済み」だったとします
        bool isEmailAlreadyRegistered = false;
        if (isEmailAlreadyRegistered)
        {
            // 第1引数に string.Empty（空文字）を指定するとModelOnly 用のエラーとして追加する
            ModelState.AddModelError(string.Empty,
                "このメールアドレスは既に登録されています。" +
                "別のメールアドレスをお試しください。");
            
            return View(model);
        }
        TempData["RegisterData"] = JsonSerializer.Serialize(model);
        return RedirectToAction("Confirm");
    }

    // ==========================================
    // 3. 確認画面（GET）
    // ==========================================
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

    // ==========================================
    // 4. 確認画面（POST）→ 完了画面へ
    // ==========================================
    [HttpPost]
    public IActionResult ConfirmPost()
    {
        if (!TempData.ContainsKey("RegisterData"))
        {
            return RedirectToAction("Index");
        }

        // TempDataから安全にデータを復元する
        var json = TempData["RegisterData"]!.ToString()!;
        var model = JsonSerializer.Deserialize<RegisterUserInputModel>(json);

        // 完了画面へデータを渡すために、別の名前で保存し直す
        TempData["RegisterData"] = JsonSerializer.Serialize(model);

        return RedirectToAction("Complete");
    }

    // ==========================================
    // 5. 完了画面（GET）
    // ==========================================
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