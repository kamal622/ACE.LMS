using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class LibraryBook
    {
        public LibraryBook()
        {
            this.BookIssues = new List<BookIssue>();
            this.BookRequestDetails = new List<BookRequestDetail>();
            this.LibraryBookHistories = new List<LibraryBookHistory>();
        }

        public int Id { get; set; }
        public Nullable<int> BookId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public Nullable<int> PublishedYear { get; set; }
        public Nullable<int> Status { get; set; }
        public string BookNo { get; set; }
        public Nullable<int> ClassNo { get; set; }
        public string AddedBY { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string RemovedBy { get; set; }
        public Nullable<System.DateTime> RemovedOn { get; set; }
        public string ReasonForRemoving { get; set; }
        public string Notes { get; set; }
        public bool HasCD { get; set; }
        public string AccessionNo { get; set; }
        public bool IsVerified { get; set; }
        public Nullable<int> Pages { get; set; }
        public string ISBN { get; set; }
        public string Store { get; set; }
        public virtual ICollection<BookIssue> BookIssues { get; set; }
        public virtual ICollection<BookRequestDetail> BookRequestDetails { get; set; }
        public virtual Book Book { get; set; }
        public virtual ICollection<LibraryBookHistory> LibraryBookHistories { get; set; }
    }
}
