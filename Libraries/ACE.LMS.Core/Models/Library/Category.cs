using System;
using System.Collections.Generic;

namespace ACE.LMS.Core.Models.Library
{
    public class Category : BaseEntity
    {
        public Category()
        {
            this.BookCategories = new List<BookCategory>();
        }

        //public int Id { get; set; }
        public Nullable<int> ParentCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
    }
}
