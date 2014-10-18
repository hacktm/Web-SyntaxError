using System;

namespace CollegeBuffer.DAL.Model
{
    public sealed class GroupRequest : AbstractModel
    {
        public DateTime? Date { get; set; }

        public User User { get; set; }

        public Group Group { get; set; }
    }
}