using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Models.Security;
using System.ComponentModel.DataAnnotations;

namespace ACE.LMS.Core.Models.Library
{
    public partial class BookRequest : BaseEntity
    {
        public BookRequest()
        {
            this.BookRequestDetails = new List<BookRequestDetail>();
            this.NewBookRequestDetails = new List<NewBookRequestDetail>();
        }

        //public int Id { get; set; }
        public int StudentId { get; set; }
        public int CollegeId { get; set; }
        public int BranchId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Semester begins on is required")]
        [Display(Name = "Semester begins on:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime SemesterStartDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Semester ends on is required")]
        [Display(Name = "Semester ends on:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime SemesterEndDate { get; set; }

        [Display(Name = "Do you stay in hostel ?:")]
        public bool IsHostelStay { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address when studying is required")]
        [Display(Name = "Address when studying:")]
        [DataType(DataType.MultilineText)]
        public string AddressWhenStudy { get; set; }

        public string Subject { get; set; }
        public System.DateTime RequestDate { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual AspNetUser CreatedByUser { get; set; }
        public virtual AspNetUser UpdatedUser { get; set; }
        public virtual ICollection<BookRequestDetail> BookRequestDetails { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual College College { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<NewBookRequestDetail> NewBookRequestDetails { get; set; }

    }
}
