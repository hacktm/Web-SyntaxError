using System.Collections.ObjectModel;
using CollegeBuffer.DAL.Model.Abstract;
using CollegeBuffer.DAL.Model.Enums;

namespace CollegeBuffer.DAL.Model
{
    public class User : AbstractModel
    {
        public User()
        {
            GroupsAsStudent = new Collection<Group>();    
            GroupsAsAdministrator = new Collection<Group>();
            GroupRequests = new Collection<GroupRequest>();
            Subjects = new Collection<Subject>();
            Comments = new Collection<Comment>();
            Announcements = new Collection<Announcement>();
            AnnouncementNotifications = new Collection<AnnouncementNotification>();
            EventNotifications = new Collection<EventNotification>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }

        public string Gender { get; set; }

        public byte[] ImageData { get; set; }

        public UserRoles Role { get; set; }

        public virtual Session Session { get; set; }

        public virtual Collection<Group> GroupsAsStudent { get; set; }

        public virtual Collection<Group> GroupsAsAdministrator { get; set; }

        public virtual Collection<GroupRequest> GroupRequests { get; set; }

        public virtual Collection<Subject> Subjects { get; set; }

        public virtual Collection<Comment> Comments { get; set; }

        public virtual Collection<Announcement> Announcements { get; set; }

        public virtual Collection<AnnouncementNotification> AnnouncementNotifications { get; set; }

        public virtual Collection<EventNotification> EventNotifications { get; set; } 
    }
}