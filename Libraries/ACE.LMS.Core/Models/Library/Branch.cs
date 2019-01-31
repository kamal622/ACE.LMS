using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ACE.LMS.Core.Models.Library
{
    public partial class Branch : BaseEntity
    {
        public Branch()
        {
            this.BookRequests = new List<BookRequest>();

        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Branch is required")]
        [Display(Name = "Branch:")]
        [StringLength(128, ErrorMessage = "The branch must be at least {2} characters long.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public virtual ICollection<BookRequest> BookRequests { get; set; }
    }
}
