using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class BookRequestDetail
    {
        public BookRequestDetail()
        {
            this.BookIssues = new List<BookIssue>();
            this.BookRequestHistories = new List<BookRequestHistory>();
        }

        public int Id { get; set; }
        public int RequestId { get; set; }
        public int BookId { get; set; }
        public Nullable<int> Status { get; set; }
        public string Subject { get; set; }
        public Nullable<int> BookAssigned { get; set; }
        public Nullable<System.DateTime> AvailableFrom { get; set; }
        public string Notes { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<BookIssue> BookIssues { get; set; }
        public virtual BookRequest BookRequest { get; set; }
        public virtual Book Book { get; set; }
        public virtual LibraryBook LibraryBook { get; set; }
        public virtual ICollection<BookRequestHistory> BookRequestHistories { get; set; }
    }
}
