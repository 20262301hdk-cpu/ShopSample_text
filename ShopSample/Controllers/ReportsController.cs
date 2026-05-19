using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSample.Data;
using ShopSample.ViewModels;

namespace ShopSample.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Reports — ナビゲーション画面
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ProductList()
        {
            var products = await _context.Products
                .Where(p => p.Price >= 1000)
                .Include(p => p.Category)
                .ToListAsync();
            var viewModel = new ProductListViewModel
            {
                PageTitle = "商品一覧（1,000円以上）",
                QueryDescription = "Where を使って価格が 1,000 円以上の商品を絞り込んでいます。",
                Products = products
            };
            return View("ProductList", viewModel);
        }
        public async Task<IActionResult> ProductSorted()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.CategoryId)
                    .ThenBy(p => p.Price)
                .ToListAsync();
            var viewModel = new ProductListViewModel
            {
                PageTitle = "商品一覧（カテゴリ順・価格安い順）",
                QueryDescription = "OrderBy と ThenBy を使ってカテゴリ順 → 価格の安い順に並べ替えています。",
                Products = products
            };
            return View("ProductList", viewModel);
        }
        public async Task<IActionResult> ProductStats()
        {
            var totalCount = await _context.Products.CountAsync();
            var foodCount = await _context.Products
                .CountAsync(p => p.CategoryId == 1);
            var hasExpensive = await _context.Products
                .AnyAsync(p => p.Price >= 1000);

            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            var viewModel = new ProductListViewModel
            {
                PageTitle = "商品統計",
                QueryDescription = "CountAsync と AnyAsync を使って統計情報を取得しています。",
                Products = products,
                TotalCount = totalCount,
                FilteredCount = foodCount,
                HasExpensiveProducts = hasExpensive
            };

            return View("ProductList", viewModel);
        }
        public async Task<IActionResult> ProductPriceSum()
        {
            var totalPriceSum = await _context.Products
                .SumAsync(p => p.Price);

            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            var viewModel = new ProductListViewModel
            {
                PageTitle = "商品一覧（価格合計付き）",
                QueryDescription = "SumAsync を使って全商品の価格合計を計算しています。",
                Products = products,
                TotalPriceSum = totalPriceSum
            };

            return View("ProductList", viewModel);
        }
        public async Task<IActionResult> OrderHistory()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var orderTotals = new Dictionary<int, int>();
            foreach (var order in orders)
            {
                orderTotals[order.Id] = order.OrderDetails
                    .Sum(od => od.Quantity * od.UnitPrice);
            }
            var grandTotal = orderTotals.Values.Sum();

            var viewModel = new OrderHistoryViewModel
            {
                PageTitle = "購入履歴",
                QueryDescription = "ThenInclude で注文 → 明細 → 商品の" +
                "多階層データを取得し、Sum で合計を計算しています。",
                Orders = orders,
                OrderTotals = orderTotals,
                GrandTotal = grandTotal
            };

            return View("OrderHistory", viewModel);
        }
        public async Task<IActionResult> CategorySummary()
        {
            var summaries = await _context.Products
                .GroupBy(p => p.Category.Name)
                .Select(g => new CategorySummaryItem
                {
                    CategoryName = g.Key,
                    ProductCount = g.Count(),
                    AveragePrice = (int)g.Average(p => p.Price),
                    TotalPrice = g.Sum(p => p.Price)
                })
                .ToListAsync();

            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            var viewModel = new ProductListViewModel
            {
                PageTitle = "カテゴリ別集計",
                QueryDescription = "GroupBy を使ってカテゴリ別の商品数・" +
                "平均価格・合計金額を集計しています。",
                Products = products,
                CategorySummaries = summaries
            };

            return View("ProductList", viewModel);
        }
        public async Task<IActionResult> OrderHistoryFiltered()
        {
            var orders = await _context.Orders
                .Where(o => o.OrderDate >= new DateTime(2024, 11, 1))
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var orderTotals = new Dictionary<int, int>();
            foreach (var order in orders)
            {
                orderTotals[order.Id] = order.OrderDetails
                    .Sum(od => od.Quantity * od.UnitPrice);
            }
            var grandTotal = orderTotals.Values.Sum();

            var viewModel = new OrderHistoryViewModel
            {
                PageTitle = "購入履歴（2024 年 11 月以降）",
                QueryDescription = "Include → ThenInclude → Where → " +
                "OrderByDescending を組み合わせた複合クエリです。",
                Orders = orders,
                OrderTotals = orderTotals,
                GrandTotal = grandTotal
            };

            return View("OrderHistory", viewModel);
        }

    }
}
