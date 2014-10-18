using CollegeBuffer.BLL.Repositories;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL
{
    public class DbUnitOfWork : BaseUnitOfWork
    {
        public static DbUnitOfWork NewInstance()
        {
            return new DbUnitOfWork();
        }

        #region Fields

        private UsersRepository _usersRepository;
        private GroupsRepository _groupsRepository;

        #endregion

        public UsersRepository UsersRepository
        {
            get
            {
                return _usersRepository ?? (_usersRepository = new UsersRepository(DbContext));
            }
        }

        public GroupsRepository GroupsRepository
        {
            get
            {
                return _groupsRepository ?? (_groupsRepository = new GroupsRepository(DbContext));
            }
        }

        #region properties

        #endregion
    }
}