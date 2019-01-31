using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class BookRequestHistory
    {
        public int Id { get; set; }
        public Nullable<int> BookRequestDetailId { get; set; }
        public Nullable<int> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual BookRequestDetail BookRequestDetail { get; set; }
    }
}
