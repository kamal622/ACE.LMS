using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ACE.LMS.Core.Models.Library
{
    public partial class NoticeBoard : BaseEntity
    {
        //public int Id { get; set; }

        [Required(ErrorMessage = "Please enter title")]
        [Display(Name = "Title:")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select start date")]
        [Display(Name = "Start Date:")]
        public System.DateTime StartDate { get; set; }

        [Display(Name = "End Date:")]
        public Nullable<System.DateTime> EndDate { get; set; }

        [Display(Name = "Is Active:")]
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
