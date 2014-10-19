using System;
using System.Collections.ObjectModel;
using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.DAL.Model
{
    public class Announcement : AbstractModel
    {
        public Announcement()
        {
            Comments = new Collection<Comment>();   
        }

        public DateTime? Date { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public virtual Group Group { get; set; }

        public virtual User User { get; set; }

        public virtual Collection<Comment> Comments { get; set; } 
    }
}