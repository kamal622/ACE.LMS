using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class College
    {
        public College()
        {
            this.BookRequests = new List<BookRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public bool HasBookBank { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public virtual ICollection<BookRequest> BookRequests { get; set; }
    }
}
