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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
