using System.Collections.ObjectModel;

namespace CollegeBuffer.DAL.Model
{
    public sealed class Group : AbstractModel
    {
        public Group()
        {
            Users = new Collection<User>();    
            Administrators = new Collection<User>();  
            SubGroups = new Collection<Group>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] ImageData { get; set; }

        public Collection<User> Users { get; set; }

        public Collection<User> Administrators { get; set; }

        public Collection<Group> SubGroups { get; set; }

        public Group SuperGroup { get; set; }

        public Collection<GroupRequest> GroupRequests { get; set; }
    }
}