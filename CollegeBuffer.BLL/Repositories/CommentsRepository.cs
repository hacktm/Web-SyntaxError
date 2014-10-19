using System.Linq;
using CollegeBuffer.BLL.Interfaces;
using CollegeBuffer.DAL.Context;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Repositories
{
    public class CommentsRepository : BaseRepository<Comment>, ICommentsRepository
    {
        public CommentsRepository(DatabaseContext context) : base(context)
        {
        }

        public bool SafeDeleteComment(Comment comment)
        {
            if (@comment.Replies.Count <= 0) return Delete(@comment);

            return @comment.Replies.All(SafeDeleteComment) && Delete(@comment);
        }
    }
}