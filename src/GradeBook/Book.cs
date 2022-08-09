using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook
{
    //Creating delegate with name of GradeAddedDelegate
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    // Base class
    public class NamedObject 
    {

        public NamedObject(string name)  //Constuctor
        {
           Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook    // Interface
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }

        event GradeAddedDelegate GradeAdded;
        
    }

    public abstract class Book : NamedObject, IBook    // Abstract class
    {
        public Book(string name) : base(name)
        {

        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);   // Abstract method
        public abstract Statistics GetStatistics();

    }


    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {

        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt")) //using will automatically dispose the data once usage is done
            {
                writer.WriteLine(grade);

                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();

                }
            }
            return result;
        }
    }

    
     //Derived className : Base ClassName

    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name) // Accessing base class Constructor
        {   
            grades = new List<double>();
            Name = name;
        }
        public override void AddGrade(double grade)    // Overriding the abstract method
        {
            if(grade <= 100 && grade >= 0)
            { 
                grades.Add(grade);

                if(GradeAdded != null) // If the GradeAdded event is not null
                {
                    GradeAdded(this, new EventArgs()); //Calling event with sender and event args
                }
                
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        // Creating event with name of GradeAdded
        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            
            for(var index=0; index < grades.Count; index += 1)
            {
                result.Add(grades[index]);               
            }

            return result;
        }

        private List<double> grades;
        

       public const string CATEGORY = "Science";  // Const variable
    }
}
