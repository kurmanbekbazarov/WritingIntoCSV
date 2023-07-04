using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Class
{
    public List<Student> Roster { get; set; }
}

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<TestResult> Grades { get; set; }
}

public class TestResult
{
    public string TestTitle { get; set; }
    public double Score { get; set; }
}

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the number of students: ");
            int numberOfStudents = Convert.ToInt32(Console.ReadLine());

            Class ClassRoster = new Class();
            ClassRoster.Roster = new List<Student>(); 

            for (int i = 0; i < numberOfStudents; i++)
            {
                var student = new Student();
                Console.WriteLine($"Please enter details on student {i + 1}: " );
                Console.WriteLine("Enter the first name: ");
                student.FirstName = Console.ReadLine();

                Console.WriteLine("Enter the last name: ");
                student.LastName = Console.ReadLine();

                Console.WriteLine("How many tests are there? ");
                int numberOfTests = Convert.ToInt32(Console.ReadLine());
                student.Grades = new List<TestResult>(); 

                for (int j = 0; j < numberOfTests; j++)
                {
                    var test = new TestResult();
                    Console.WriteLine($"Please enter details on test {j + 1}: ");
                    Console.WriteLine("Enter the test title: ");
                    test.TestTitle = Console.ReadLine();

                    Console.WriteLine("Enter the test score: ");
                    test.Score = Convert.ToDouble(Console.ReadLine());
                    student.Grades.Add(test); 
                }
                ClassRoster.Roster.Add(student); 
            }

            string filename = "CourseName.csv";
            WriteToCSV(ClassRoster, filename); 

            Console.WriteLine("Data written to the CSV file successfully!");
            Console.ReadLine();

            // ClearCsvFile(filename);
        }

        private static void ClearCsvFile(string fileName)
        {
            // Open the file in write mode and truncate its content
            using (var writer = new StreamWriter(fileName))
            {
                writer.Write(string.Empty);
            }
        }

        private static void WriteToCSV(Class ClassRoster, string filename)
        {
            bool fileExists = File.Exists(filename);

            using (var writer = new StreamWriter(filename, append: true))
            {
                if (!fileExists)
                {
                    writer.WriteLine("SrNo, FirstName, LastName, TestTitle, Score"); 
                }

                int srNo = 1; 
                
                foreach (var student in ClassRoster.Roster)
                {
                    foreach (var grade in student.Grades)
                    {
                        writer.WriteLine($"{srNo}, {student.FirstName}, {student.LastName}, {grade.TestTitle}, {grade.Score}");
                        srNo++; 
                    }
                }
            }
        }
    }
}