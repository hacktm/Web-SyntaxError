using System.Data.Entity;
using CollegeBuffer.DAL.Context;

namespace CollegeBuffer.DAL.ContextInitializers
{
    internal class DatabaseContextInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        public override void InitializeDatabase(DatabaseContext context)
        {
            base.InitializeDatabase(context);
        }
    }
}