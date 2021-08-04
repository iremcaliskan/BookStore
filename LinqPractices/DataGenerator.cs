using Bogus;
using System.Collections.Generic;
using System.Linq;

namespace LinqPractices
{
    public class DataGenerator
    {
        public static void Initialize()
        {
            using (var context = new StudentDbContext())
            {
                if (context.Students.Any())
                {
                    return;
                }

                var students = new List<Student>();
                for (int i = 1; i < 101; i++) // 100 fake Student
                {
                    students.Add(
                        new Faker<Student>()
                        .RuleFor(x => x.Name, f => f.Name.FirstName())
                        .RuleFor(x => x.Surname, f => f.Name.LastName())
                        .RuleFor(x => x.ClassId, f => f.Random.Number(1, 30)));
                }
                context.Students.AddRange(students);
                context.SaveChanges();
            }
        }
    }
}