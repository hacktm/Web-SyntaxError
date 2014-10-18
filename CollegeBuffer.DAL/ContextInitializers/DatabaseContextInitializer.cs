#define TESTS
//#undef TESTS
using System.Data.Entity;
using CollegeBuffer.DAL.Context;

namespace CollegeBuffer.DAL.ContextInitializers
{
#if TESTS

    internal class DatabaseContextInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);
        }
    }

#else
    internal class DatabaseContextInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);
        }
    }

#endif
}