using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class City
    {
        public City()
        {
            this.Students = new List<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
