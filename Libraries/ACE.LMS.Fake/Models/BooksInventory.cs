using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class BooksInventory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Total { get; set; }
        public int Available { get; set; }
        public virtual Book Book { get; set; }
    }
}
