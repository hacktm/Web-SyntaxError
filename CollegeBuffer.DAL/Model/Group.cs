using System.Collections.ObjectModel;
using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.DAL.Model
{
    public class Group : AbstractModel
    {
        public Group()
        {
            Users = new Collection<User>();    
            Administrators = new Collection<User>();  
            SubGroups = new Collection<Group>();
            GroupRequests = new Collection<GroupRequest>();
            Subjects = new Collection<Subject>();
            Announcements = new Collection<Announcement>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] ImageData { get; set; }

        public virtual Group SuperGroup { get; set; }

        public virtual Collection<Event> Events { get; set; }

        public virtual Collection<User> Users { get; set; }

        public virtual Collection<User> Administrators { get; set; }

        public virtual Collection<Group> SubGroups { get; set; }

        public virtual Collection<GroupRequest> GroupRequests { get; set; }

        public virtual Collection<Subject> Subjects { get; set; }

        public virtual Collection<Announcement> Announcements { get; set; }

    }
}