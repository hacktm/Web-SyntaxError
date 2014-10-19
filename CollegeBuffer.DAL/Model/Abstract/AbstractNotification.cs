namespace CollegeBuffer.DAL.Model.Abstract
{
    public class AbstractNotification : AbstractModel
    {
        public string Message { get; set; }

        public virtual User User { get; set; }
    }
}