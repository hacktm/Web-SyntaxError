using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.BLL.Interfaces
{
    internal interface ICommentsRepository
    {
        bool SafeDeleteComment(Comment comment);
    }
}
