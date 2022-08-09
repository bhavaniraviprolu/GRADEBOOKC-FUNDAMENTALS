using System;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book("Bhavani Grade Book");
/*
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.5);
*/

            while(true)
            {
                Console.WriteLine("Enter a grade or 'q' for quit");

                var input = Console.ReadLine();

                if(input == "q")
                { 
                  break;
                }

                try 
                { 
                  var grade = double.Parse(input);
                  book.AddGrade(grade);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                   
                }
            }

            var stats = book.GetStatistics();

            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
           
        }
    }
}
