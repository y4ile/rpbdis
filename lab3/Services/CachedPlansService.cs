using lab2.db.classes;
using lab2.db.views;
using lab2.db;
using Microsoft.Extensions.Caching.Memory;

namespace lab3.Services
{
    public class CachedPlansService : ICachedPlansService
    {
        private readonly PlansDBContext _dbContext;
        private readonly IMemoryCache _cache;
        private readonly int _rowsNumber;
        private readonly int _cacheDurationSeconds;

        public CachedPlansService(PlansDBContext dbContext, IMemoryCache memoryCache, int variantNumber = 15)
        {
            _dbContext = dbContext;
            _cache = memoryCache;
            _rowsNumber = 20;
            _cacheDurationSeconds = 2 * variantNumber + 240; // 2*N + 240 секунд
        }

        // Метод для получения данных из таблицы с кэширование
        public IEnumerable<T> GetTableData<T>(string cacheKey) where T : class
        {
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<T> cachedData))
            {
                // Получение данных из базы данных
                var dbSet = _dbContext.Set<T>();
                cachedData = dbSet.Take(_rowsNumber).ToList();

                if (cachedData != null)
                {
                    // Установка данных в кэш
                    _cache.Set(cacheKey, cachedData, TimeSpan.FromSeconds(_cacheDurationSeconds));
                }
            }
            return cachedData;
        }

        // Метод для предварительного заполнения кэша для всех таблиц
        public void PreloadCache()
        {
            // Список таблиц и соответствующих кэш-ключей
            var cacheItems = new List<(string CacheKey, Type EntityType)>
            {
                ("DevelopmentDirections", typeof(DevelopmentDirection)),
                ("PlanStages", typeof(PlanStage)),
                ("PlanStagesWithInfos", typeof(PlanStagesWithInfo)),
                ("Statuses", typeof(Status)),
                ("StudyPlans", typeof(StudyPlan)),
                ("StudyPlanWithUserInfos", typeof(StudyPlanWithUserInfo)),
                ("Users", typeof(User)),
                ("UsersWithStudyPlans", typeof(UsersWithStudyPlan))
            };

            foreach (var item in cacheItems)
            {
                // Используем метод GetTableData для заполнения кэша
                var method = typeof(CachedPlansService).GetMethod(nameof(GetTableData)).MakeGenericMethod(item.EntityType);
                method.Invoke(this, new object[] { item.CacheKey });
            }
        }
    }
}
