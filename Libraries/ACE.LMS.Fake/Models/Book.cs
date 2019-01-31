using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class Book
    {
        public Book()
        {
            this.BookCategories = new List<BookCategory>();
            this.BookRequestDetails = new List<BookRequestDetail>();
            this.LibraryBooks = new List<LibraryBook>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
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
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
        public virtual ICollection<BookRequestDetail> BookRequestDetails { get; set; }
        public virtual ICollection<LibraryBook> LibraryBooks { get; set; }
    }
}
