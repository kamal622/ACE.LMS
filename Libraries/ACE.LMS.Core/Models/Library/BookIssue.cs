using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.LMS.Core.Models.Library
{
    public partial class BookIssue:BaseEntity
    {
        public BookIssue()
        {
            this.BookReturns = new List<BookReturn>();
        }

      //  public int Id { get; set; }
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
        public Nullable<int> StudentId { get; set; }


        public virtual BookRequestDetail BookRequestDetail { get; set; }
        public virtual LibraryBook LibraryBook { get; set; }
        public virtual ICollection<BookReturn> BookReturns { get; set; }

        public virtual Student Student { get; set; }

    }
}
