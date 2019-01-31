using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class BookIssue
    {
        public BookIssue()
        {
            this.BookReturns = new List<BookReturn>();
        }

        public int Id { get; set; }
        public Nullable<int> BookRequestDetailsId { get; set; }
        public Nullable<int> Status { get; set; }
        public string Notes { get; set; }
        public Nullable<int> LibraryBooksId { get; set; }
        public string IssuedBy { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> ReturnOnOrBefore { get; set; }
        public string BookReceivedBy { get; set; }
        public string ReceiverNo { get; set; }
        public virtual BookRequestDetail BookRequestDetail { get; set; }
        public virtual LibraryBook LibraryBook { get; set; }
        public virtual ICollection<BookReturn> BookReturns { get; set; }
    }
}
