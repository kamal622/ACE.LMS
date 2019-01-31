using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class Branch
    {
        public Branch()
        {
            this.BookRequests = new List<BookRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public virtual ICollection<BookRequest> BookRequests { get; set; }
    }
}
