using Answer_Validator.DB_Classes;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Net.Http.Headers;
namespace Answer_Validator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new TestingDbContext();

            LoginDetails(db);
            ReturnTestResults(db);

            string username = RegistrationForm.UserNameAccount;

            SendResultsToDB(username, db);

            Console.ReadKey();
        }

        //  ------- Signing in to account to complete a test application --------
        static void LoginDetails(TestingDbContext db)
        {
            new RegistrationForm().AddUserToApplication(db);
        }

        //  ------- Getting results after testing  --------
        static void ReturnTestResults(TestingDbContext db)
        {
            int countCorrectAns = new TestApplication().CheckAnswers();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(new string('-', 40) + "\nNumber of correct answers : {0}",countCorrectAns);
            Console.WriteLine(new string('-', 40));

            Console.ResetColor();
        }

        //  ------- Sending result to the database  --------
        static void SendResultsToDB(string user, TestingDbContext db)
        {
            var username = db.Users.FirstOrDefault(u => u.UserName == user);
            if (user != null)
            {
                int countCorrectAns = TestApplication.NumberOfCorrectAnswers;
                var result = new Result()
                {
                    ResultValue = countCorrectAns,
                    UserId = username.Id
                };

                db.Results.Add(result);
                db.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Test result successfully added to the database for user: " +
                    "" + username.UserName);
                Console.ResetColor();
            }
        }
    }
}
