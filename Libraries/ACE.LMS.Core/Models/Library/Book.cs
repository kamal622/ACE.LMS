using ACE.LMS.Core.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ACE.LMS.Core.Models.Library
{
    public partial class Book : BaseEntity
    {
        public Book()
        {
            this.BookRequestDetails = new List<BookRequestDetail>();
           
            this.LibraryBooks = new List<LibraryBook>();
     
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        [Display(Name = "Title:")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Author is required")]
        [Display(Name = "Author:")]
        public string Author { get; set; }

        public string Subject { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Publication is required")]
        [Display(Name = "Publisher:")]
        public string Publication { get; set; }
        public bool IsNewRequest { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> TotalBooks { get; set; }
        public Nullable<int> ApprovedBooks { get; set; }
        public Nullable<int> NewApprovedBooks { get; set; }
        public Nullable<int> IssuedBooks { get; set; }
        public Nullable<int> TornBooks { get; set; }
        public Nullable<int> AvailableBooks { get; set; }
      
        public virtual AspNetUser CreatedByUser { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
        public virtual ICollection<BookRequestDetail> BookRequestDetails { get; set; }
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
   
    }
}
