using System.Collections.ObjectModel;
using CollegeBuffer.DAL.Model.Enums;

namespace CollegeBuffer.DAL.Model
{
    public class User : AbstractModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }

        public string Gender { get; set; }

        public byte[] ImageData { get; set; }

        public virtual Collection<Group> GroupsAsStudent { get; set; }

        public virtual Collection<Group> GroupsAsAdministrator { get; set; }

        public UserRoles Role { get; set; }
    }
}