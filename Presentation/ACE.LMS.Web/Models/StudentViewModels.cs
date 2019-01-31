using ACE.LMS.Web.App_Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACE.LMS.Web.Models
{
    public class StudentViewModels
    {
        public class BookRequestDetailsData
        {
            public int Id { get; set; }
            public int RequestId { get; set; }
            public int BookId { get; set; }

            public int? BookRequestDetailsId { get; set; }
            public string BookTitle { get; set; }
            public string BookAuthor { get; set; }
            public string BookPublisher { get; set; }
            public string Subject { get; set; }

            public string AccessNo { get; set; }
            public DateTime RequestDate { get; set; }
            public Nullable<DateTime> IssueDate { get; set; }
            public Nullable<DateTime> ReturnDate { get; set; }
            public Nullable<DateTime> ReturnBefore { get; set; }

            public string Status { get; set; }
            public bool returnStatus { get; set; }
        }
    }
}