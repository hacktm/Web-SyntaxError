using System.Data.Entity;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.ContextInitializers
{
    public static class GroupsContextInitializer
    {
        public static void InitializeContext(DbModelBuilder builder)
        {
            builder.Entity<Group>().HasKey(k => k.Id);

            builder.Entity<Group>().Property(p => p.Name).IsRequired();
            builder.Entity<Group>().Property(p => p.Description).IsOptional();
            builder.Entity<Group>().Property(p => p.ImageData).IsOptional();

            // Map normal group students to this table
            builder.Entity<Group>()
                .HasMany(p => p.Users)
                .WithMany(p => p.GroupsAsStudent)
                .Map(m =>
                {
                    m.ToTable("Users_Groups");
                    m.MapLeftKey("GroupId");
                    m.MapRightKey("UserId");
                });

            // Map administrator group students to this table
            builder.Entity<Group>()
                .HasMany(p => p.Administrators)
                .WithMany(p => p.GroupsAsAdministrator)
                .Map(m =>
                {
                    m.ToTable("Administrators_Groups");
                    m.MapLeftKey("GroupId");
                    m.MapRightKey("UserId");
                });
        }
    }
}