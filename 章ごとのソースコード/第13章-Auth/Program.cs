using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopSample.Data;

var builder = WebApplication.CreateBuilder(args);

// リソースファイルが「Resources」フォルダに入っていることをシステムに教える
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// MVCの機能を追加する際に、一括設定のルールをねじ込む
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        // 「データアノテーションの辞書は、問答無用で SharedResource を使う」という強制ルール
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(ShopSample.SharedResource));
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

// EF Core: ApplicationDbContext をDIコンテナに登録
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity: ユーザー認証の設定
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;  // 本番環境ではtrueにすること
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Cookie認証のパス設定（AddIdentity はデフォルトパスを自動設定しないため手動で指定）
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/Login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // UseAuthorizationより前に配置すること
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
