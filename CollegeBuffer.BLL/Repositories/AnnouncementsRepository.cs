using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Repositories
{
    public class AnnouncementsRepository : BaseRepository<Announcement>, IAnnouncementsRepository
    {
        public AnnouncementsRepository(DatabaseContext context) : base(context)
        {
        }
    }
}