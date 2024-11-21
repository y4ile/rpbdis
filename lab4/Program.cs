using lab4.Middleware;
using lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// [INFO] Добавление сервисов в контейнер
builder.Services.AddControllersWithViews(options =>
{
    options.CacheProfiles.Add("PlansCacheProfile", new CacheProfile
    {
        Duration = 2 * 15 + 240,
        Location = ResponseCacheLocation.Any,
        NoStore = false
    });
});

// [INFO] Настройка подключения к базе данных
builder.Services.AddDbContext<PlansContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// [INFO] Добавление сервиса кэширования ответов
builder.Services.AddResponseCaching();

var app = builder.Build();

// [INFO] Использование middleware для инициализации базы данных
//app.UseMiddleware<DatabaseSeederMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// [INFO] Использование кэширования ответов
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
