using lab2.db;
using lab2.db.classes;
using lab2.db.views;
using lab3.Services;
using lab3.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text;
using System.Threading.Tasks;

namespace Plans
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<PlansDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddMemoryCache();

            // Добавление поддержки сессии
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            // Регистрация CachedPlansService
            int variantNumber = 15;
            builder.Services.AddScoped<ICachedPlansService>(provider =>
            {
                var dbContext = provider.GetRequiredService<PlansDBContext>();
                var memoryCache = provider.GetRequiredService<IMemoryCache>();
                return new CachedPlansService(dbContext, memoryCache, variantNumber);
            });

            var app = builder.Build();

            // Предварительная загрузка кэша
            using (var scope = app.Services.CreateScope())
            {
                var cachedService = scope.ServiceProvider.GetRequiredService<ICachedPlansService>();
                cachedService.PreloadCache();
            }

            // Добавление Middleware для работы с сессиями
            app.UseSession();

            // Вывод информации о клиенте
            app.Map("/info", PlansMiddleware.ClientInfo);

            // Вывод таблиц
            app.Map("/table", PlansMiddleware.TableData);

            // Формы поиска
            PlansMiddleware.SearchForm(app);

            app.Run();
        }
    }
}