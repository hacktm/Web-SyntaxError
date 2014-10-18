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
        private GroupRequestsRepository _groupRequestsRepository;
        private SubjectsRepository _subjectsRepository;
        private CommentsRepository _commentsRepository;

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

        public GroupRequestsRepository GroupRequestsRepository
        {
            get
            {
                return _groupRequestsRepository ?? (_groupRequestsRepository = new GroupRequestsRepository(DbContext));
            }
        }

        public SubjectsRepository SubjectsRepository
        {
            get
            {
                return _subjectsRepository ?? (_subjectsRepository = new SubjectsRepository(DbContext));
            }
        }

        public CommentsRepository CommentsRepository
        {
            get
            {
                return _commentsRepository ?? (_commentsRepository = new CommentsRepository(DbContext));
            }
        }

        #endregion
    }
}