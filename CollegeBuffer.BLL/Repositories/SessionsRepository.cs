using System;
using System.Linq;
using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Special;

namespace CollegeBuffer.BLL.Repositories
{
    public class SessionsRepository : BaseRepository<Session>, ISessionsRepository
    {
        public SessionsRepository(DatabaseContext context)
            : base(context)
        {
        }

        /// <summary>
        ///     Get the user that has the selected session details
        /// </summary>
        /// <param name="sessionId">The Id of the session</param>
        /// <param name="sessionKey">The Key for the session</param>
        /// <returns>An UserAccount instance or null if there is no session for the user</returns>
        public User GetUser(Guid sessionId, string sessionKey)
        {
            try
            {
                // ReSharper disable once PossibleNullReferenceException
                return DbSet.FirstOrDefault(s => s.Id == sessionId && s.SessionKey == sessionKey).User;
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     Open a session for a certain user
        /// </summary>
        /// <param name="username">The nickname of the user who wants to log in</param>
        /// <returns>A Session instance or a null value if it doesn't exist</returns>
        public Session Login(string username)
        {
            try
            {
                var session = new Session
                {
                    User = DbContext.Users.First(u => u.Username == username),
                    SessionKey = TokenGenerator.EncryptMd5(DateTime.Now.ToFileTime().ToString())
                };

                return Insert(session);
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     Close a session for a certain user
        /// </summary>
        /// <param name="userName">The nickname of the user who wants to log out</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool Logout(string userName)
        {
            var session = DbSet.FirstOrDefault(t => t.User.Username == userName);

            return Delete(session);
        }
    }
}