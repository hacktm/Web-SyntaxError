using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Repositories
{
    public class EventsRepository : BaseRepository<Event>, IEventsRepository
    {
        public EventsRepository(DatabaseContext context) : base(context)
        {
        }
    }
}