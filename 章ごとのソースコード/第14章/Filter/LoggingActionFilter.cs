using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ShopSample.Filter
{
    public class LoggingActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            Console.WriteLine($"[LOG] {controller}/{action} 開始 - {DateTime.Now:HH:mm:ss}");

            // Stopwatch を開始し、OnActionExecuted で取り出せるよう Items に保存
            context.HttpContext.Items["Stopwatch"] = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            if (context.HttpContext.Items["Stopwatch"] is Stopwatch sw)
            {
                sw.Stop();
                Console.WriteLine($"[LOG] {controller}/{action} " +
                    $"完了 - {sw.ElapsedMilliseconds}ms");
            }
        }
    }
}
