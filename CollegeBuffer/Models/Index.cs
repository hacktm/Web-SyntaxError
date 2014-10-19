using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.Models
{
    public class Index
    {
        public List<Announcement> Announcements { get; set; }

        public List<Event> Events { get; set; } 

        public String Error = null;

    }
}