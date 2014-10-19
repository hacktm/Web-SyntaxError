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

            return groups.OrderBy(g => g.Name).ToArray();
        }

        public bool SafeDeleteGroup(Group group)
        {
            if (@group.SubGroups.Count <= 0) return Delete(@group);
            
            return @group.SubGroups.All(SafeDeleteGroup) && Delete(@group);
        }
    }
}