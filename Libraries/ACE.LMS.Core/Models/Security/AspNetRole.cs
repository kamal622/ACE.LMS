using System;
using System.Collections.Generic;

namespace ACE.LMS.Core.Models.Security
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            this.AspNetUsers = new List<AspNetUser>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
