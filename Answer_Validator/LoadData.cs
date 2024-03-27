using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Answer_Validator
{
    public class LoadData
    {
        private string pathToQuestionsFile = @"C:\Final_Projects\01_FinalProject\Answer_Validator\txt_files\questions.txt";

        private string pathToAnswersFiles = @"C:\Final_Projects\01_FinalProject\Answer_Validator\txt_files\answers.txt";

        public Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> ReadQuestionsFromFile()
        {
            if(File.Exists(pathToQuestionsFile)) {

                string[] listQuestions = File.ReadAllLines(pathToQuestionsFile); //Question 
                string[] listAnswersLines = File.ReadAllLines(pathToAnswersFiles); //Answers

                if (listQuestions.Length != listAnswersLines.Length) {
                    throw new ArgumentException("Number of questions does not match number of answers.");
                }


                for (int i = 0; i < listQuestions.Length; i++)
                {
                    string question = listQuestions[i];
                    string[] answers = listAnswersLines[i].Split(',').Select(a => a.Trim()).ToArray();

                    if (answers.Length != 4)
                    {
                        throw new ArgumentException($"Incorrect number of answers for question {i + 1}");
                    }
                    dict.Add(question, new List<string>(answers));
                }

                return dict;
            }

            throw new FileNotFoundException("Given path is not valid");
        }
    }
}
