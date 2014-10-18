using System;

namespace CollegeBuffer.DAL.Model.Abstract
{
    public abstract class AbstractEvent : AbstractModel
    {
        public DateTime? DateCreated { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? NotificationStartDate { get; set; }
    }
}