using System.Data.Entity;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.ContextInitializers
{
    public static class GroupRequestsContextInitializer
    {
        public static void InitializeContext(DbModelBuilder builder)
        {
            builder.Entity<GroupRequest>().HasKey(k => k.Id);

            builder.Entity<GroupRequest>().Property(p => p.Date).IsRequired();

            // Map to the Groups table
            builder.Entity<GroupRequest>()
                .HasRequired(p => p.Group)
                .WithMany(p => p.GroupRequests)
                .Map(m => m.MapKey("GroupId"))
                .WillCascadeOnDelete(true);

            // Map to the Users table
            builder.Entity<GroupRequest>()
                .HasRequired(p => p.User)
                .WithMany(p => p.GroupRequests)
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(true);
        }
    }
}