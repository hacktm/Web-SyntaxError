using System.Collections.ObjectModel;
using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.DAL.Model
{
    public class Calendar : AbstractModel
    {
        public virtual User User { get; set; }

        public virtual Collection<CalendarEntry> Entries { get; set; } 
    }
}