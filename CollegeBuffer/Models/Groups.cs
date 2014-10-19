using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.Models
{
    public class Groups
    {
        public Groups()
        {
            GroupPath = new List<Group>();
        }
        public List<Group> GroupPath { get; set; }
        public Group[] ChildGroups { get; set; }
    }
}