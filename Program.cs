/*
    [LAB 2] @author Yahor Leanovich
*/

using lab2.db;
using lab2.db.classes;
using lab2.db.views;
using lab2.db.operations;
using Microsoft.IdentityModel.Tokens;

namespace lab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (PlansDBContext dbContext = new PlansDBContext())
            {
                // Task 3.2.1
                Console.WriteLine("lab2\n\nPress any key to process SELECT #1 operation");
                Console.ReadKey();
                Operations.SelectAllUsers(dbContext);

                // Task 3.2.2
                Console.WriteLine("\nPress any key to process SELECT #2 operation");
                Console.ReadKey();
                Operations.SelectDirectionsAbove(dbContext, 450);

                // Task 3.2.3
                Console.WriteLine("\nPress any key to process SELECT #3 operation");
                Console.ReadKey();
                Operations.SelectStagesPriorityAmount(dbContext);

                // Task 3.2.4
                Console.WriteLine("\nPress any key to process SELECT #4 operation");
                Console.ReadKey();
                Operations.SelectPlansWithUsers(dbContext);

                // Task 3.2.5
                Console.WriteLine("\nPress any key to process SELECT #5 operation");
                Console.ReadKey();
                Operations.SelectPlansWithUserIDAbove(dbContext, 450);

                // Task 3.2.6
                /*
                Console.WriteLine("\nPress any key to process INSERT #1 operation");
                Console.ReadKey();
                string login = "";
                while (login.IsNullOrEmpty())
                {
                    Console.WriteLine("\nEnter new user login:");
                    login = Console.ReadLine();
                }
                string pass = "";
                while (pass.IsNullOrEmpty())
                {
                    Console.WriteLine("\nEnter new user password:");
                    pass = Console.ReadLine();
                }
                string email = "";
                while (email.IsNullOrEmpty())
                {
                    Console.WriteLine("\nEnter new user email:");
                    email = Console.ReadLine();
                }
                Operations.InsertUser(dbContext, login, pass, email);
                Operations.SelectUserByEmail(dbContext, email);
                */

                // Task 3.2.7
                /*
                Console.WriteLine("\nPress any key to process INSERT #2 operation");
                Console.ReadKey();
                string planName = "";
                while (planName.IsNullOrEmpty())
                {
                    Console.WriteLine("\nEnter new study plan name:");
                    planName = Console.ReadLine();
                }
                int directionId = -1;
                while (directionId < 0)
                {
                    Console.WriteLine("\nEnter new study plan directionId:");
                    directionId = Convert.ToInt32(Console.ReadLine());
                }
                int userId = -1;
                while (userId < 0)
                {
                    Console.WriteLine("\nEnter new study plan userId:");
                    userId = Convert.ToInt32(Console.ReadLine());
                }
                Operations.InsertStudyPlan(dbContext, planName, directionId, userId);
                Operations.SelectPlanByName(dbContext, planName);
                */

                // Task 3.2.8
                Console.WriteLine("\nPress any key to process DELETE #1 operation\nAll linked values in other tables also will be deleted!");
                Console.ReadKey();
                Console.WriteLine("Users size before: " + dbContext.Users.ToArray().Length.ToString());
                int userId = -1;
                while (userId < 0)
                {
                    Console.WriteLine("\nEnter userId to delete:");
                    userId = Convert.ToInt32(Console.ReadLine());
                }
                Operations.DeleteUser(dbContext, userId);
                Console.WriteLine("Users size after: " + dbContext.Users.ToArray().Length.ToString());

                // Task 3.2.9
                Console.WriteLine("\nPress any key to process DELETE #2 operation");
                Console.ReadKey();
                Console.WriteLine("StudyPlans size before: " + dbContext.StudyPlans.ToArray().Length.ToString());
                string planName = "";
                while (planName.IsNullOrEmpty())
                {
                    Console.WriteLine("\nEnter plan name to delete:");
                    planName = Console.ReadLine();
                }
                Operations.DeletePlan(dbContext, planName);
                Console.WriteLine("StudyPlans size after: " + dbContext.StudyPlans.ToArray().Length.ToString());

                // Task 3.2.10
                Console.WriteLine("\nPress any key to process UPDATE operation");
                Console.ReadKey();
                string email = "";
                while (email.IsNullOrEmpty())
                {
                    Console.WriteLine("\nEnter user email to update:");
                    email = Console.ReadLine();
                }
                string login = "";
                while (login.IsNullOrEmpty())
                {
                    Console.WriteLine("\nEnter new user login:");
                    login = Console.ReadLine();
                }
                if (!Operations.UpdateUserLoginByEmail(dbContext, email, login))
                    Console.WriteLine("User not found!");

                Console.WriteLine("Press any key to end application...");
                Console.ReadKey();
            }
        }
    }
}