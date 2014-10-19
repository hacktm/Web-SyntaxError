using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Interfaces
{
    internal interface IGroupRequestsRepository
    {
        GroupRequest[] GetRequestsForGroup(Group group);

        GroupRequest[] GetRequestsMadeByUser(User user);
    }
}
