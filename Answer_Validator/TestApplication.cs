using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answer_Validator
{
    /// <summary>
    /// This class is for reading data from the file and creating a test for our application
    /// Also one of class methods returns a number of correct users answers
    /// </summary>
    public class TestApplication
    {
        public readonly string[] correctAnswers = { "A", "A", "D", "B", "A" };
        private string[] TestApplicationMethod()
        {

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('*', 40) + "\nWelcome to the test application. In this test" +
                " you gonna find out your geography knowledge\n\n");
            Console.ResetColor();

            Thread.Sleep(500);

            var testLibrary = new LoadData().ReadQuestionsFromFile();


            List<string> usersAnswers = new List<string>();

            foreach (var testElement in testLibrary)
            {
                string question = testElement.Key;
                List<string> answers = testElement.Value;

                Console.WriteLine($"Question {usersAnswers.Count + 1} : {question}");
                Console.WriteLine(string.Join("   ", answers.Select((answer, i) => $"{answer}")));

                // Checking if correct answers array contains an input value 
                string userAnswer;
                do
                {
                    Console.WriteLine("\nEnter your answer:");
                    userAnswer = Console.ReadLine().Trim().ToUpper();

                    if (userAnswer != "A" && userAnswer != "B" && userAnswer != "C" && userAnswer != "D")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input! Please enter A, B, C, or D.");

                        Console.ResetColor();
                    }
                } while (userAnswer != "A" && userAnswer != "B" && userAnswer != "C" && userAnswer != "D");

                usersAnswers.Add(userAnswer);
                Console.WriteLine();
            }
            return usersAnswers.ToArray();
        }

        public static int NumberOfCorrectAnswers { get; private set; }
        public int CheckAnswers()
        {
            string[] usersAnswers = TestApplicationMethod();

            var matchedAnswers = correctAnswers
                .Zip(usersAnswers, (expected, actual) => expected == actual ? expected : null)
                .Where(answer => answer != null).Count();

            NumberOfCorrectAnswers = (int)matchedAnswers;
            return (int)matchedAnswers;
        }
    }
}
