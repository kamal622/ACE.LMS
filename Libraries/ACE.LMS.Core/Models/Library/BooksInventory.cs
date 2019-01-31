using System;
using System.Collections.Generic;

namespace ACE.LMS.Core.Models.Library
{
    public partial class BooksInventory : BaseEntity
    {
        //public int Id { get; set; }
        public int BookId { get; set; }
        public int Total { get; set; }
        public int Available { get; set; }
        public virtual Book Book { get; set; }
    }
}
