using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class NewBookRequestDetail
    {
        public int Id { get; set; }
        public int BookRequestId { get; set; }
        public string Subject { get; set; }
        public string BookTitle { get; set; }
        public string Publication { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public virtual BookRequest BookRequest { get; set; }
    }
}
