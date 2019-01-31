using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ACE.LMS.Core.Models.Library
{
    public partial class College : BaseEntity
    {
        public College()
        {
            this.BookRequests = new List<BookRequest>();
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "College name is required")]
        [Display(Name = "Name of College:")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "College address is required")]
        [Display(Name = "Address:")]
        public string Address { get; set; }
        public string City { get; set; }

        [Display(Name = "Do college has book bank ?:")]
        public bool HasBookBank { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public virtual ICollection<BookRequest> BookRequests { get; set; }
       
    }
}
