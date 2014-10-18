using System.Data.Entity;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.ContextInitializers
{
    public static class SubjectsContextInitializer
    {
        public static void InitializeContext(DbModelBuilder builder)
        {
            builder.Entity<Subject>().HasKey(k => k.Id);

            builder.Entity<Subject>().Property(p => p.Name).IsRequired();
            builder.Entity<Subject>().Property(p => p.Description).IsOptional();
            builder.Entity<Subject>().Property(p => p.ImageData).IsOptional();

            // Map to the Users table
            builder.Entity<Subject>()
                .HasMany(p => p.Users)
                .WithMany(p => p.Subjects)
                .Map(m =>
                {
                    m.ToTable("Subjects_Users");
                    m.MapLeftKey("SubjectId");
                    m.MapRightKey("UserId");
                });

            // Map to the Groups table
            builder.Entity<Subject>()
                .HasMany(p => p.Groups)
                .WithMany(p => p.Subjects)
                .Map(m =>
                {
                    m.ToTable("Subjects_Groups");
                    m.MapLeftKey("SubjectId");
                    m.MapRightKey("GroupId");
                });
        }
    }
}