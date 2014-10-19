using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Interfaces
{
    internal interface IUsersRepository
    {
        User[] FilterUsers(string phrase);

        bool VerifyExists(string username);

        bool VerifyPassword(string username, string password);
    }
}
