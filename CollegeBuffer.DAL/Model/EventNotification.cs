using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.DAL.Model
{
    public class EventNotification : AbstractNotification
    {
        public const string Title = "New Event!";

        public virtual Event Event { get; set; }
    }
}