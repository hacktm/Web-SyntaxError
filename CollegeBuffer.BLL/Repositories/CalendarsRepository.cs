using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Repositories
{
    public class CalendarsRepository : BaseRepository<Calendar>, ICalendarsRepository
    {
        public CalendarsRepository(DatabaseContext context) : base(context)
        {
        }
    }
}