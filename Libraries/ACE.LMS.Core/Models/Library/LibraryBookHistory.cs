using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Models.Security;

namespace ACE.LMS.Core.Models.Library
{
    public partial class LibraryBookHistory:BaseEntity
    {
        //public int Id { get; set; }
        public Nullable<int> LibraryBookId { get; set; }
        public Nullable<int> Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual LibraryBook LibraryBook { get; set; }
    }
}
