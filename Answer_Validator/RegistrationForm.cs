using Answer_Validator.DB_Classes;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answer_Validator
{
    /// <summary>
    /// This class is for creating a user account or checking if the person is 
    /// already in database
    /// </summary>
    public class RegistrationForm
    {
        public static bool ContainsLetters(string str)
        {
            return str.Any(char.IsLetter);
        }

        public static string UserNameAccount { get; private set; }
        public static string ReturnUserName()
        {
            Console.WriteLine("To start a test application, enter a username to log in or register..");
            string? userName = Console.ReadLine();

            if (userName != null && ContainsLetters(userName))
            {

                UserNameAccount = userName;
                return userName;
            }
            return "Username can NOT be null and must contain at least 3 letters";
        }
        public void AddUserToApplication(TestingDbContext db)
        {
            string username = ReturnUserName();
            var existingUser = db.Users.FirstOrDefault(u => u.UserName == username);

            if (existingUser == null)
            {
                Console.WriteLine("\nEnter valid email to register your account :");
                string email = Console.ReadLine();

                var newUser = new AppUser()
                {
                    UserName = username,
                    Email = email,
                    DateOfRegistration = DateTime.Now
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Connection with database were successfully established and " +
                    "a new user was added to a database");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou are already registered. Press any key to start a test");

                Console.ResetColor();
                Console.ReadKey();

                Thread.Sleep(1500);
            }
        }
    }
}
