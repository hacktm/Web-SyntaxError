using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.Models
{
    public class MyGroups
    {
        public Collection<Group> GroupsAsAdmin { get; set; }
        public Collection<Group> GroupsAsUser { get; set; }
    }
}