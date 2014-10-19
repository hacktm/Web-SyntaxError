using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.Models
{
    public class GroupPage
    {
        public GroupPage ()
        {
            Announcements = new Collection<Announcement>();
            Events = new Collection<Event>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public string GroupId { get; set; }

        public Collection<Announcement> Announcements { get; set; } 
        public Collection<Event> Events { get; set; }

        public Announcement NewAnnouncement { get; set; }

    }
}