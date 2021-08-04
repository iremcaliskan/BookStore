using System;
using System.Linq;

namespace EntityViewModelDtoPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            DataGenerator.Initialize();
            var context = new SchoolDbContext();
            var students = context.Students.ToList<Student>();
            var grades = context.Grades.ToList<Grade>();

            foreach (var item in students)
            {
                Console.WriteLine(item.StudentID + " " + item.StudentName + " " + item.Photo + " " + item.DateOfBirth
                    + " " + item.Height + " " + item.Weight);
            }
            foreach (var item in grades)
            {
                Console.WriteLine(item.GradeId + " " + item.GradeName + " " + item.Section);
            }
            Console.WriteLine("Student List Count: " + students.Count);
            Console.WriteLine("Grade List Count: " + grades.Count);
        }
    }
}