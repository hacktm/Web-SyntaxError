using System;
using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.DAL.Model
{
    public class AnnouncementNotification : AbstractNotification
    {
        public const string Title = "New Announcement!";

        public virtual Announcement Announcement { get; set; }
    }
}