using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.LMS.Core.Models.Library
{
    public partial class BookReturn:BaseEntity
    {
      //  public int Id { get; set; }
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
