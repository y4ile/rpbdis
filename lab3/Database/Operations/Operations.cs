using lab2.db.classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.db.operations
{
    internal class Operations
    {
        public static void SelectAllUsers(PlansDBContext dbContext)
        {
            var queryLinq = from u in dbContext.Users select new { UserID = u.UserId, Login = u.Login, Pass = u.Password, Email = u.Email };
            Console.WriteLine("All db.Users rows:");
            foreach (var row in queryLinq)
            {
                Console.WriteLine(row.ToString());
            }
        }

        public static void SelectDirectionsAbove(PlansDBContext dbContext, int aboveValue = 100)
        {
            var queryLinq = from d in dbContext.DevelopmentDirections
                            where (d.DirectionId > aboveValue)
                            select new
                            {
                                DirectionID = d.DirectionId,
                                Name = d.DirectionName
                            };

            Console.WriteLine("All db.DevelopmentDirections rows where ID > " + aboveValue.ToString() + ":");
            foreach (var row in queryLinq)
            {
                Console.WriteLine(row.ToString());
            }
        }

        public static void SelectStagesPriorityAmount(PlansDBContext dbContext)
        {
            var queryLinq = from s in dbContext.PlanStages
                            group s.StageId by s.Priority into pr
                            select new
                            {
                                Priority = pr.Key,
                                Amount = pr.Count()
                            };

            Console.WriteLine("Priorities & it's linked stages amount:");
            foreach (var row in queryLinq)
            {
                Console.WriteLine(row.ToString());
            }
        }

        public static void SelectPlansWithUsers(PlansDBContext dbContext)
        {
            var queryLinq = from p in dbContext.StudyPlans
                            join u in dbContext.Users
                            on p.UserId equals u.UserId
                            select new
                            {
                                PlandID = p.PlanId,
                                Email = u.Email
                            };

            Console.WriteLine("Plans & it's users:");
            foreach (var row in queryLinq)
            {
                Console.WriteLine(row.ToString());
            }
        }

        public static void SelectPlansWithUserIDAbove(PlansDBContext dbContext, int aboveValue = 100)
        {
            var queryLinq = from p in dbContext.StudyPlans
                            join u in dbContext.Users
                            on p.UserId equals u.UserId
                            where (u.UserId > aboveValue)
                            select new
                            {
                                PlandID = p.PlanId,
                                UserID = u.UserId,
                                Email = u.Email
                            };

            Console.WriteLine("Plans & it's users with ID > " + aboveValue.ToString() + ":");
            foreach (var row in queryLinq)
            {
                Console.WriteLine(row.ToString());
            }
        }

        public static void SelectUserByEmail(PlansDBContext dbContext, string email)
        {
            var queryLinq = from u in dbContext.Users
                            where (u.Email == email)
                            select new
                            {
                                UserID = u.UserId,
                                Login = u.Login,
                                Email = u.Email
                            };

            Console.WriteLine("Selected users:");
            foreach (var row in queryLinq)
            {
                Console.WriteLine(row.ToString());
            }
        }

        public static void InsertUser(PlansDBContext dbContext, string login, string pass, string email)
        {
            User user = new User
            {
                Login = login,
                Password = pass,
                Email = email
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public static void SelectPlanByName(PlansDBContext dbContext, string name)
        {
            var queryLinq = from p in dbContext.StudyPlans
                            join u in dbContext.Users
                            on p.UserId equals u.UserId
                            join d in dbContext.DevelopmentDirections
                            on p.DirectionId equals d.DirectionId
                            where (p.PlanName == name)
                            select new
                            {
                                PlanID = p.PlanId,
                                Name = p.PlanName,
                                UserEmail = u.Email,
                                Direction = d.DirectionName
                            };

            Console.WriteLine("Selected plans:");
            foreach (var row in queryLinq)
            {
                Console.WriteLine(row.ToString());
            }
        }

        public static void InsertStudyPlan(PlansDBContext dbContext, string name, int directionId, int userId)
        {
            StudyPlan studyPlan = new StudyPlan
            {
                PlanName = name,
                DirectionId = directionId,
                UserId = userId
            };

            dbContext.StudyPlans.Add(studyPlan);
            dbContext.SaveChanges();
        }

        public static void DeleteUser(PlansDBContext dbContext, int userId)
        {
            var user = dbContext.Users.Where(u => u.UserId == userId);
            dbContext.Users.RemoveRange(user);
            dbContext.SaveChanges();
        }

        public static void DeletePlan(PlansDBContext dbContext, string name)
        {
            var plan = dbContext.StudyPlans.Where(p => p.PlanName == name);
            dbContext.StudyPlans.RemoveRange(plan);
            dbContext.SaveChanges();
        }

        public static bool UpdateUserLoginByEmail(PlansDBContext dbContext, string email, string newLogin)
        {
            var user = dbContext.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
                user.Login = newLogin;
            else
                return false;

            dbContext.SaveChanges();
            return true;
        }
    }
}
