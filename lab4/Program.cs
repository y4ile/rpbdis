using lab4.Middleware;
using lab4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// [INFO] ���������� �������� � ���������
builder.Services.AddControllersWithViews(options =>
{
    options.CacheProfiles.Add("PlansCacheProfile", new CacheProfile
    {
        Duration = 2 * 15 + 240,
        Location = ResponseCacheLocation.Any,
        NoStore = false
    });
});

// [INFO] ��������� ����������� � ���� ������
builder.Services.AddDbContext<PlansContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// [INFO] ���������� ������� ����������� �������
builder.Services.AddResponseCaching();

var app = builder.Build();

// [INFO] ������������� middleware ��� ������������� ���� ������
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

// [INFO] ������������� ����������� �������
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
