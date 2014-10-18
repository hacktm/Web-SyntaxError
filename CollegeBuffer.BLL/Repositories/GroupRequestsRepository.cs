using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Repositories
{
    public class GroupRequestsRepository : BaseRepository<GroupRequest>, IGroupRequestsRepository
    {
        public GroupRequestsRepository(DatabaseContext context) : base(context)
        {
        }
    }
}