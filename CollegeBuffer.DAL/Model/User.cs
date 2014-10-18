using System.Collections.ObjectModel;
using CollegeBuffer.DAL.Model.Enums;

namespace CollegeBuffer.DAL.Model
{
    public sealed class User : AbstractModel
    {
        public User()
        {
            GroupsAsStudent = new Collection<Group>();    
            GroupsAsAdministrator = new Collection<Group>();    
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }

        public string Gender { get; set; }

        public byte[] ImageData { get; set; }

        public UserRoles Role { get; set; }

        public Collection<Group> GroupsAsStudent { get; set; }

        public Collection<Group> GroupsAsAdministrator { get; set; }

        public Session Session { get; set; }

        public Collection<GroupRequest> GroupRequests { get; set; }

        public Collection<Subject> Subjects { get; set; }
    }
}