using System;
using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.DAL.Model
{
    public class GroupRequest : AbstractModel
    {
        public DateTime? Date { get; set; }

        public virtual User User { get; set; }

        public virtual Group Group { get; set; }
    }
}