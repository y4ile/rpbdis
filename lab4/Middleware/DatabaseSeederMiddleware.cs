using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using lab4.Models;
using System;
using System.Linq;

namespace lab4.Middleware
{
    public class DatabaseSeederMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DatabaseSeederMiddleware> _logger;

        public DatabaseSeederMiddleware(RequestDelegate next, ILogger<DatabaseSeederMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, PlansContext dbContext)
        {
            if (!dbContext.DevelopmentDirections.Any())
            {
                _logger.LogInformation("Seeding the database with test data...");

                // [INFO] Добавление DevelopmentDirections
                var directions = new[]
                {
                    new DevelopmentDirection { DirectionName = "Направление A" },
                    new DevelopmentDirection { DirectionName = "Направление B" },
                    new DevelopmentDirection { DirectionName = "Направление C" }
                };
                dbContext.DevelopmentDirections.AddRange(directions);
                dbContext.SaveChanges();

                // [INFO] Добавление Users
                var users = new[]
                {
                    new User { Login = "user1", Password = "password1", Email = "user1@example.com" },
                    new User { Login = "user2", Password = "password2", Email = "user2@example.com" }
                };
                dbContext.Users.AddRange(users);
                dbContext.SaveChanges();

                // [INFO] Добавление StudyPlans
                var studyPlans = new[]
                {
                    new StudyPlan { PlanName = "Plan 1", DirectionId = directions[0].DirectionID, UserId = users[0].UserID },
                    new StudyPlan { PlanName = "Plan 2", DirectionId = directions[1].DirectionID, UserId = users[1].UserID },
                    new StudyPlan { PlanName = "Plan 3", DirectionId = directions[2].DirectionID, UserId = users[0].UserID }
                };
                dbContext.StudyPlans.AddRange(studyPlans);
                dbContext.SaveChanges();

                // [INFO] Добавление Statuses
                var statuses = new[]
                {
                    new Status { StatusName = "Open" },
                    new Status { StatusName = "In Progress" },
                    new Status { StatusName = "Closed" }
                };
                dbContext.Statuses.AddRange(statuses);
                dbContext.SaveChanges();

                // [INFO] Добавление PlanStages
                var planStages = new[]
                {
                    new PlanStage
                    {
                        StageName = "Stage 1",
                        PlanId = studyPlans[0].PlanID,
                        StatusId = statuses[0].StatusId,
                        Priority = 1,
                        OpenDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10)),
                        CloseDate = null
                    },
                    new PlanStage
                    {
                        StageName = "Stage 2",
                        PlanId = studyPlans[1].PlanID,
                        StatusId = statuses[1].StatusId,
                        Priority = 2,
                        OpenDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)),
                        CloseDate = null
                    },
                    new PlanStage
                    {
                        StageName = "Stage 3",
                        PlanId = studyPlans[2].PlanID,
                        StatusId = statuses[2].StatusId,
                        Priority = 3,
                        OpenDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)),
                        CloseDate = DateOnly.FromDateTime(DateTime.Now)
                    }
                };
                dbContext.PlanStages.AddRange(planStages);
                dbContext.SaveChanges();

                _logger.LogInformation("Database seeding completed.");
            }

            await _next(context);
        }
    }
}
