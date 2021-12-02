using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Users
    {
        public static void AddUsers(this BookStoreDbContext context) // extends BookStoreDbContext
        {
            context.Users.AddRange(
                new User { Name = "İrem", Surname = "Çalışkan", Email = "iremcaliskan0@gmail.com", Password = "123123123"},
                new User { Email = "test@test.com", Password = "123123123"});
        }
    }
}