//#define TESTS
#undef TESTS
using System;
using System.Data.Entity;
using System.Linq;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Enums;
using CollegeBuffer.DAL.Special;

namespace CollegeBuffer.DAL.ContextInitializers
{
#if TESTS

    internal class DatabaseContextInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {

#else

    internal class DatabaseContextInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
#endif

        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);

            CreateImplicitAdmin();
        }

        private static void CreateImplicitAdmin()
        {
            var admin = new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Password = TokenGenerator.EncryptMd5("admin"),
                EMail = "",
                Role = UserRoles.Administrator
            };

            using (var db = new DatabaseContext())
            {
                if (db.Users.Any(u => u.Role == UserRoles.Administrator)) return;
                
                admin = db.Users.Add(admin);

                if (admin == null || db.SaveChanges() == 0)
                    throw new ApplicationException("The admin account could not be initialized!");
            }
        }
    }

}