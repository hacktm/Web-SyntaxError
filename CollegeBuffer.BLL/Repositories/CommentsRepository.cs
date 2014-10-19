using System.Linq;
using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;
using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.BLL.Repositories
{
    public class CommentsRepository : BaseRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(DatabaseContext context) : base(context)
        {
        }
    }
}