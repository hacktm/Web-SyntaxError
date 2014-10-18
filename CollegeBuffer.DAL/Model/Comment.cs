using System;
using System.Collections.ObjectModel;

namespace CollegeBuffer.DAL.Model
{
    public sealed class Comment : AbstractModel
    {
        public Comment()
        {
            Replies = new Collection<Comment>();    
        }

        public DateTime? Date { get; set; }

        public string Text { get; set; }

        public User User { get; set; }

        public Comment ParentComment { get; set; }

        public Collection<Comment> Replies { get; set; } 
    }
}