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
            AdministrativeRole = false;
        }
        public List<Group> GroupPath { get; set; }
        public Group[] ChildGroups { get; set; }

        public bool AdministrativeRole { get; set; }
 
    }
}