using ACE.LMS.Core.Models.Library;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ACE.LMS.Core.Models.Directory
{
    public partial class City : BaseEntity
    {
        public City()
        {
            this.Students = new List<Student>();
        }

        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
