using BookCatalogue.Data;
using BookCatalogue.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ─────────────
// In-Memory データベースを登録
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("BooksDb"));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// ─────────────
// 初回起動時にサンプルデータをシード
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();  // In-Memory DB にはマイグレーション不要
    if (!db.Books.Any())
    {
        db.Books.AddRange(new[]
        {
            new Book
            {
                Title = "サンプル1",
                Author = "太郎",
                PublishedDate = DateTime.Today,
                Price = 1000
            },
            new Book
            {
                Title = "サンプル2",
                Author = "花子",
                PublishedDate = DateTime.Today,
                Price = 1000
            }
        });
        db.SaveChanges();
    }
}

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
