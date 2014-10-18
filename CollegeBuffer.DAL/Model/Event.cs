using System;
using System.Collections.ObjectModel;
using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.DAL.Model
{
    public class Event : AbstractModel
    {
        public Event()
        {
            Subjects = new Collection<Subject>();
        }

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? NotificationStartDate { get; set; }

        public string Place { get; set; }

        public virtual Collection<Subject> Subjects { get; set; } 
    }
}