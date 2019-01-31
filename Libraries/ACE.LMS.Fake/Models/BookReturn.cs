using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class BookReturn
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public Nullable<int> BookIssueId { get; set; }
        public string Notes { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> LateDays { get; set; }
        public Nullable<decimal> FineCollected { get; set; }
        public string SubmittedBy { get; set; }
        public string Mobile { get; set; }
        public virtual BookIssue BookIssue { get; set; }
    }
}
