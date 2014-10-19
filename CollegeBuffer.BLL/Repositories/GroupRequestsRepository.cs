using System.Linq;
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

        public GroupRequest[] GetRequestsForGroup(Group group)
        {
            var requests = DbSet.Where(r => r.Group.Id == group.Id);

            return requests.ToArray();
        }

        public GroupRequest[] GetRequestsMadeByUser(User user)
        {
            var requests = DbSet.Where(r => r.User.Id == user.Id);

            return requests.ToArray();
        }
    }
}