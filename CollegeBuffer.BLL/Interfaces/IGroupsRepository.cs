using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Interfaces
{
    internal interface IGroupsRepository
    {
        Group[] GetAllSuperGroups();

        bool SafeDeleteGroup(Group group);
    }
}