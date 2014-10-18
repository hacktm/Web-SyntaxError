using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Repositories
{
    public class EventNotificationsRepository : BaseRepository<EventNotification>, IEventNotificationsRepository
    {
        public EventNotificationsRepository(DatabaseContext context) : base(context)
        {
        }
    }
}