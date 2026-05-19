using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShopSample.Filter
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // エラー情報をログに記録
            _logger.LogError(
                context.Exception,
                "エラーが発生しました: {Controller}/{Action}",
                context.RouteData.Values["controller"],
                context.RouteData.Values["action"]);

            // ユーザーにカスタムエラーページを表示
            context.Result = new ViewResult
            {
                ViewName = "CustomError"
            };

            // 例外を処理済みとしてマーク（これがないと例外が再スローされる）
            context.ExceptionHandled = true;
        }
    }

}
