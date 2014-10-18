using System.Linq;
using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Repositories
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(DatabaseContext context) : base(context)
        {
        }

        public bool VerifyExists(string username)
        {
            var user = DbSet.FirstOrDefault(u => u.Username == username);

            return user != null;
        }

        public bool VerifyPassword(string username, string password)
        {
            var user = DbSet.FirstOrDefault(u => u.Username == username);

            return user != null && user.Password == password;
        }
    }
}