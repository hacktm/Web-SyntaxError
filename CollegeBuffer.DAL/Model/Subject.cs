using System.Collections.ObjectModel;
using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.DAL.Model
{
    public class Subject : AbstractModel
    {
        public Subject()
        {
            Users = new Collection<User>();
            Groups = new Collection<Group>();
            Events = new Collection<Event>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] ImageData { get; set; }

        public virtual Collection<User> Users { get; set; }

        public virtual Collection<Group> Groups { get; set; }

        public virtual Collection<Event> Events { get; set; } 
    }
}