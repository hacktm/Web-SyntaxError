using System.Data.Entity;
using CollegeBuffer.DAL.ContextInitializers;

namespace CollegeBuffer.DAL.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = true;

            Database.SetInitializer(new DatabaseContextInitializer());
        }
    }
}