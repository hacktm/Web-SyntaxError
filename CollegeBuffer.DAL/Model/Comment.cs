using System;
using System.Collections.ObjectModel;
using CollegeBuffer.DAL.Model.Abstract;

namespace CollegeBuffer.DAL.Model
{
    public class Comment : AbstractModel
    {
        public Comment()
        {
            Replies = new Collection<Comment>();    
        }

        public DateTime? Date { get; set; }

        public string Text { get; set; }

        public virtual User User { get; set; }

        public virtual Comment ParentComment { get; set; }

        public virtual Collection<Comment> Replies { get; set; } 
    }
}