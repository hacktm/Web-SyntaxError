using System.Data.Entity;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.ContextInitializers
{
    internal static class UsersContextInitializer
    {
        internal static void InitializeContext(DbModelBuilder builder)
        {
            builder.Entity<User>().HasKey(k => k.Id);

            builder.Entity<User>().Property(p => p.Username).IsRequired();
            builder.Entity<User>().Property(p => p.Password).IsRequired();
            builder.Entity<User>().Property(p => p.EMail).IsRequired();
            builder.Entity<User>().Property(p => p.Role).IsRequired();

            builder.Entity<User>().Property(p => p.FirstName).IsOptional();
            builder.Entity<User>().Property(p => p.LastName).IsOptional();
            builder.Entity<User>().Property(p => p.Gender).IsOptional();
            builder.Entity<User>().Property(p => p.ImageData).IsOptional();

            builder.Entity<User>().Property(p => p.Gender).HasColumnType("char").HasMaxLength(1);

            // Map to the Session table
            builder.Entity<User>()
                .HasOptional(p => p.Session)
                .WithOptionalPrincipal(p => p.User)
                .Map(m => m.MapKey("UserId"));
        }
    }
}