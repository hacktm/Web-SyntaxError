using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Repositories
{
    public class AnnouncementNotificationsRepository : BaseRepository<AnnouncementNotification>, IAnnouncementNotificationsRepository
    {
        public AnnouncementNotificationsRepository(DatabaseContext context) : base(context)
        {
        }
    }
}