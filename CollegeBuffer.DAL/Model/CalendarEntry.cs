using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.DAL.Model
{
    public class CalendarEntry : AbstractEvent
    {
        public string Text { get; set; }

        public virtual Calendar Calendar { get; set; }

        public void CopyEvent(Event ev)
        {
            DateCreated = ev.DateCreated;
            StartDate = ev.StartDate;
            EndDate = ev.EndDate;
            NotificationStartDate = ev.NotificationStartDate;
        }
    }
}