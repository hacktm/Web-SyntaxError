using System.Collections.ObjectModel;

namespace CollegeBuffer.DAL.Model
{
    public class Group : AbstractModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public byte ImageData { get; set; }

        public virtual Collection<Student> Students { get; set; }

        public virtual Collection<Student> Administrators { get; set; }

        public virtual Collection<Group> SubGroups { get; set; }

        public virtual Group SuperGroup { get; set; }
    }
}