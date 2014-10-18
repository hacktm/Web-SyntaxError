using System;
using CollegeBuffer.BLL.Repositories;
using CollegeBuffer.DAL.Context;

namespace CollegeBuffer.BLL
{
    public class DbUnitOfWork : BaseUnitOfWork
    {
        public DbUnitOfWork() : base()
        {
        }

        public static DbUnitOfWork NewInstance()
        {
            return new DbUnitOfWork();
        }

        #region Fields

        private UsersRepository _usersRepository;
        private GroupsRepository _groupsRepository;
        private SessionsRepository _sessionsRepository;

        #endregion

        #region properties

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

        public SessionsRepository SessionsRepository
        {
            get
            {
                return _sessionsRepository ?? (_sessionsRepository = new SessionsRepository(DbContext));
            }
        }

        #endregion
    }
}