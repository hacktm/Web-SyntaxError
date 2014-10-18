namespace CollegeBuffer.BLL.Interfaces
{
    internal interface IUsersRepository
    {
        bool VerifyExists(string username);

        bool VerifyPassword(string username, string password);
    }
}
