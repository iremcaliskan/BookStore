using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityViewModelDtoPractices
{
    public class DataGenerator
    {
        public static void Initialize()
        {
            using (var context = new SchoolDbContext())
            {
                if (context.Students.Any())
                {
                    return; // Eleman varsa oluşturma
                }
                // Yoksa
                var students = new List<Student>();
                for (int i = 1; i < 21; i++) // 20 fake Student
                {
                    students.Add(
                        new Faker<Student>()
                        .RuleFor(x => x.StudentName, f => f.Name.FullName())
                        .RuleFor(x => x.DateOfBirth, f => f.Date.Between(new DateTime(1997, 1, 1), new DateTime(1998, 12, 30)).ToString("dd/MM/yyyy"))
                        .RuleFor(x => x.Photo, f => f.Internet.Avatar())
                        .RuleFor(x => x.Height, f =>
                        {
                            var num = f.Random.Decimal(150.00m, 200.00m);
                            return Math.Round(num, 1);
                        })
                        .RuleFor(x => x.Weight, f =>
                        {
                            var num = f.Random.Decimal(45.00m, 120.00m);
                            return Math.Round(num, 2);
                        }));
                }

                if (context.Grades.Any())
                {
                    return; // Eleman varsa oluşturma
                }
                // Yoksa
                var grades = new List<Grade>();
                for (int i = 1; i < 41; i++) // 40 fake Grade
                {
                    grades.Add(
                        new Faker<Grade>()
                        .RuleFor(x => x.GradeName, f =>
                        {
                            var grade = f.Random.Decimal(0, 100);
                            return Math.Round(grade).ToString();
                        })
                        .RuleFor(x => x.Section, f => f.Lorem.Word()));
                }

                context.Students.AddRange(students);
                context.Grades.AddRange(grades);
                context.SaveChanges();
            }
        }
    }
}