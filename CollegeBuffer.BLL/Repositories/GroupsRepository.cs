using System.Linq;
using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Repositories
{
    public class GroupsRepository : BaseRepository<Group>, IGroupsRepository
    {
        public GroupsRepository(DatabaseContext context) : base(context)
        {
        }

        public Group[] GetAllSuperGroups()
        {
            var groups = DbSet.Where(g => g.SuperGroup == null);

            return groups.ToArray();
        }
    }
}