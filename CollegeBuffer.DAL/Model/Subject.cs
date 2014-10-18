using System.Collections.ObjectModel;

namespace CollegeBuffer.DAL.Model
{
    public sealed class Subject : AbstractModel
    {
        public Subject()
        {
            Users = new Collection<User>();
            Groups = new Collection<Group>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] ImageData { get; set; }

        public Collection<User> Users { get; set; }

        public Collection<Group> Groups { get; set; }
    }
}