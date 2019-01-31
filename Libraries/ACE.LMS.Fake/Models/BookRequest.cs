using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class BookRequest
    {
        public BookRequest()
        {
            this.BookRequestDetails = new List<BookRequestDetail>();
            this.NewBookRequestDetails = new List<NewBookRequestDetail>();
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CollegeId { get; set; }
        public int BranchId { get; set; }
        public System.DateTime SemesterStartDate { get; set; }
        public System.DateTime SemesterEndDate { get; set; }
        public bool IsHostelStay { get; set; }
        public string AddressWhenStudy { get; set; }
        public string Subject { get; set; }
        public System.DateTime RequestDate { get; set; }
        public Nullable<int> Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual ICollection<BookRequestDetail> BookRequestDetails { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual College College { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<NewBookRequestDetail> NewBookRequestDetails { get; set; }
    }
}
