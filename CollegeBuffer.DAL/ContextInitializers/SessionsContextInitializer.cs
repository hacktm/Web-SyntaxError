using System.Data.Entity;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.ContextInitializers
{
    public static class SessionsContextInitializer
    {
        public static void InitializeContext(DbModelBuilder builder)
        {
            builder.Entity<Session>()
                .HasKey(k => k.Id);

            builder.Entity<Session>()
                .Property(p => p.SessionKey).IsRequired();
        }
    }
}