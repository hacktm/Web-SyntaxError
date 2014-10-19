using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CollegeBuffer.DAL.Model;

namespace CollegeBuffer.Models
{
    public class GroupVM
    {
        public Group ParentGroup { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string ParentGroupId { get; set; }

        public Collection<User> Admins { get; set; }
    }
}