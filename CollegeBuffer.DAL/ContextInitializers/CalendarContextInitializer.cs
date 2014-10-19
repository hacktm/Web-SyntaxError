using System.Data.Entity;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.DAL.ContextInitializers
{
    public static class CalendarContextInitializer
    {
        public static void InitializeContext(DbModelBuilder builder)
        {
            InitializeCalendar(builder);
            InitializeCalendarEntries(builder);
        }

        public static void InitializeCalendar(DbModelBuilder builder)
        {
            builder.Entity<Calendar>().HasKey(k => k.Id);
        }

        public static void InitializeCalendarEntries(DbModelBuilder builder)
        {
            builder.Entity<CalendarEntry>().HasKey(k => k.Id);

            builder.Entity<CalendarEntry>().Property(p => p.Text).IsRequired();

            builder.Entity<CalendarEntry>().Property(p => p.DateCreated).IsOptional();
            builder.Entity<CalendarEntry>().Property(p => p.StartDate).IsOptional();
            builder.Entity<CalendarEntry>().Property(p => p.EndDate).IsOptional();
            builder.Entity<CalendarEntry>().Property(p => p.NotificationStartDate).IsOptional();

            // Map to the Calendar table
            builder.Entity<CalendarEntry>()
                .HasRequired(p => p.Calendar)
                .WithMany(p => p.Entries)
                .Map(m => m.MapKey("CalendarId"));
        }
    }
}