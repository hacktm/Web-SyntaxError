using System.Data.Entity;
using CollegeBuffer.DAL.ContextInitializers;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("CollegeBufferConnection")
        {
            Configuration.LazyLoadingEnabled = true;

            Database.SetInitializer(new DatabaseContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            UsersContextInitializer.InitializeContext(modelBuilder);
            GroupsContextInitializer.InitializeContext(modelBuilder);
            SessionsContextInitializer.InitializeContext(modelBuilder);
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}