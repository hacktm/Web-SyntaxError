using System;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Interfaces
{
    interface ISessionsRepository
    {
        User GetUser(Guid sessionId, string sessionKey);

        Session Login(string username);

        bool Logout(string userName);
    }
}
