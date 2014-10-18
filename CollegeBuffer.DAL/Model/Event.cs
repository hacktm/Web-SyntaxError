using System.Collections.ObjectModel;
using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.DAL.Model
{
    public class Event : AbstractEvent
    {
        public Event()
        {
            Subjects = new Collection<Subject>();
        }

        public string Title { get; set; }

        public string Message { get; set; }

        public string Place { get; set; }

        public virtual Collection<Subject> Subjects { get; set; }
    }
}