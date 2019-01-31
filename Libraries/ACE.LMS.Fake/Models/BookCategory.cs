using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class BookCategory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Category Category { get; set; }
    }
}
