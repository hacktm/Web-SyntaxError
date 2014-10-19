using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Repositories
{
    public class CalendarEntriesRespository : BaseRepository<CalendarEntry>, ICalendarEntriesRepository
    {
        public CalendarEntriesRespository(DatabaseContext context) : base(context)
        {
        }
    }
}