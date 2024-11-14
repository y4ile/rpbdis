using lab2.db.classes;
using lab2.db.views;
using lab3.Services;
using Microsoft.SqlServer.Server;
using System.Net.Security;
using System.Text;
using System.Text.Json;

namespace lab3.Middleware
{
    public class PlansMiddleware
    {
        // Получение информации о клиенте
        public static void ClientInfo(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var userAgent = context.Request.Headers["User-Agent"].ToString();
                var ip = context.Connection.RemoteIpAddress?.ToString();
                var responseText = $"<h1>Информация о клиенте</h1><p>IP: {ip}</p><p>User-Agent: {userAgent}</p>";

                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync(responseText);
            });
        }

        // Вывод таблиц базы данных
        public static void TableData(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                // Извлечение имени таблицы из URL, например: /table/Users
                var segments = context.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries);
                if (segments.Length >= 1)
                {
                    string tableName = segments[0];
                    var cachedService = context.RequestServices.GetRequiredService<ICachedPlansService>();
                    var cacheKey = tableName;
                    var cacheKeys = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "developmentdirections", typeof(DevelopmentDirection) },
                        { "planstages", typeof(PlanStage) },
                        { "statuses", typeof(Status) },
                        { "studyplans", typeof(StudyPlan) },
                        { "users", typeof(User) }
                    };
                    if (cacheKeys.TryGetValue(tableName, out Type entityType))
                    {
                        var method = typeof(CachedPlansService).GetMethod(nameof(CachedPlansService.GetTableData)).MakeGenericMethod(entityType);
                        var data = method.Invoke(cachedService, new object[] { cacheKey });

                        if (data != null)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append($"<h1>Данные таблицы: {tableName}</h1><table border='1'><tr>");

                            // Получение свойств типа для заголовков таблицы
                            var enumerable = data as IEnumerable<object>;
                            var firstItem = enumerable.FirstOrDefault();
                            if (firstItem != null)
                            {
                                var properties = firstItem.GetType().GetProperties();
                                foreach (var prop in properties)
                                {
                                    sb.Append($"<th>{prop.Name}</th>");
                                }
                                sb.Append("</tr>");

                                // Генерация строк таблицы
                                foreach (var item in enumerable)
                                {
                                    sb.Append("<tr>");
                                    foreach (var prop in firstItem.GetType().GetProperties())
                                    {
                                        var value = prop.GetValue(item, null);
                                        sb.Append($"<td>{value}</td>");
                                    }
                                    sb.Append("</tr>");
                                }
                                sb.Append("</table>");
                            }

                            context.Response.ContentType = "text/html; charset=utf-8";
                            await context.Response.WriteAsync(sb.ToString());
                            return; // Завершение обработки запроса
                        }
                        else
                        {
                            context.Response.StatusCode = 404;
                            context.Response.ContentType = "text/html; charset=utf-8";
                            await context.Response.WriteAsync("Таблица не найдена или данные не кэшированы.");
                            return;
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "text/html; charset=utf-8";
                        await context.Response.WriteAsync("Неверное имя таблицы.");
                        return;
                    }
                }
            });
        }

        // Форма поиска
        public static void SearchForm(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var requestPath = context.Request.Path.Value.ToLower();
                if (requestPath.Contains("/searchform1") || requestPath.Contains("/searchform2"))
                {
                    var isCookies = requestPath.Contains("/searchform1");
                    var cacheKey = isCookies ? "SearchForm1Data" : "SearchForm2Data";

                    if (context.Request.Method.Equals("GET"))
                    {
                        string prefilledQuery = string.Empty;
                        string prefilledQueryList = string.Empty;
                        string prefilledCategory = string.Empty;

                        if (isCookies)
                        {
                            // Получение данных из куки
                            if (context.Request.Cookies.ContainsKey("SearchForm1Data"))
                            {
                                var formData = context.Request.Cookies["SearchForm1Data"];
                                if (!string.IsNullOrEmpty(formData))
                                {
                                    try
                                    {
                                        var data = JsonSerializer.Deserialize<SearchFormData>(formData);
                                        prefilledQuery = data?.Query ?? string.Empty;
                                        prefilledQueryList = data?.QueryList ?? string.Empty;
                                        prefilledCategory = data?.Category ?? string.Empty;
                                    }
                                    catch (JsonException ex)
                                    {
                                        // Здесь просто сбросим предзаполненные данные
                                        prefilledQuery = string.Empty;
                                        prefilledQueryList = string.Empty;
                                        prefilledCategory = string.Empty;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Получение данных из сессии
                            var sessionData = context.Session.GetString("SearchForm2Data");
                            if (!string.IsNullOrEmpty(sessionData))
                            {
                                try
                                {
                                    var data = JsonSerializer.Deserialize<SearchFormData>(sessionData);
                                    prefilledQuery = data?.Query ?? string.Empty;
                                    prefilledQueryList = data?.QueryList ?? string.Empty;
                                    prefilledCategory = data?.Category ?? string.Empty;
                                }
                                catch (JsonException ex)
                                {
                                    // Здесь просто сбросим предзаполненные данные
                                    prefilledQuery = string.Empty;
                                    prefilledQueryList = string.Empty;
                                    prefilledCategory = string.Empty;
                                }
                            }
                        }

                        // Генерация HTML формы с предзаполненными данными
                        StringBuilder formBuilder = new StringBuilder();
                        formBuilder.Append("<!DOCTYPE html>");
                        formBuilder.Append("<html lang='ru'>");
                        formBuilder.Append("<head>");
                        formBuilder.Append("<meta charset='UTF-8'>");
                        formBuilder.Append("<title>Форма поиска</title>");
                        formBuilder.Append("</head>");
                        formBuilder.Append("<body>");
                        formBuilder.Append("<h1>Форма поиска</h1>");
                        formBuilder.Append("<form method='post' action='" + context.Request.Path + "'>");

                        // Текстовое поле
                        formBuilder.Append("<label for='query'>Поиск:</label>");
                        formBuilder.Append($"<input type='text' id='query' name='query' value='{System.Net.WebUtility.HtmlEncode(prefilledQuery)}' /><br/><br/>");

                        // Поле со списком
                        formBuilder.Append("<label for='querylist'>Таблица:</label>");
                        formBuilder.Append($"<input type='text' id='querylist' name='querylist' list='options' value='{System.Net.WebUtility.HtmlEncode(prefilledQueryList)}'>");
                        formBuilder.Append("<datalist id='options'>");
                        var tables = new List<string> { "Пользователи", "Направления", "Планы" };
                        foreach (var table in tables)
                            formBuilder.Append($"<option value='{System.Net.WebUtility.HtmlEncode(table)}'>{System.Net.WebUtility.HtmlEncode(table)}</option>");
                        formBuilder.Append("</datalist><br/><br/>");

                        // Список
                        formBuilder.Append("<label for='category'>Категория:</label>");
                        formBuilder.Append("<select id='category' name='category'>");
                        var categories = new List<string> { "Опция 1", "Опция 2", "Опция 3" }; // Заменить бы на что-то
                        foreach (var category in categories)
                        {
                            var selected = category.Equals(prefilledCategory, StringComparison.OrdinalIgnoreCase) ? "selected" : "";
                            formBuilder.Append($"<option value='{System.Net.WebUtility.HtmlEncode(category)}' {selected}>{System.Net.WebUtility.HtmlEncode(category)}</option>");
                        }
                        formBuilder.Append("</select><br/><br/>");

                        // Кнопка
                        formBuilder.Append("<button type='submit'>Найти</button>");
                        formBuilder.Append("</form>");
                        formBuilder.Append("</body></html>");

                        context.Response.ContentType = "text/html; charset=utf-8";
                        await context.Response.WriteAsync(formBuilder.ToString());
                        return;
                    }
                    else if (context.Request.Method.Equals("POST"))
                    {
                        // Чтение данных из формы
                        var form = await context.Request.ReadFormAsync();
                        var query = form["query"].ToString();
                        var queryList = form["querylist"].ToString();
                        var category = form["category"].ToString();

                        var formData = new SearchFormData
                        {
                            Query = query,
                            QueryList = queryList,
                            Category = category
                        };

                        if (isCookies)
                        {
                            // Сохранение данных в куки
                            var serializedData = JsonSerializer.Serialize(formData);
                            context.Response.Cookies.Append("SearchForm1Data", serializedData, new CookieOptions
                            {
                                Expires = DateTimeOffset.UtcNow.AddMinutes(30),
                                HttpOnly = true,
                                IsEssential = true
                            });
                        }
                        else
                        {
                            // Сохранение данных в сессии
                            var serializedData = JsonSerializer.Serialize(formData);
                            context.Session.SetString("SearchForm2Data", serializedData);
                        }

                        // Перенаправление на GET запрос для отображения формы с предзаполненными данными
                        context.Response.Redirect(context.Request.Path);
                        return;
                    }
                }
            });
        }
    }

    // Класс для хранения данных формы
    public class SearchFormData
    {
        public string Query { get; set; }
        public string QueryList { get; set; }
        public string Category { get; set; }
    }
}
