using System.ComponentModel.DataAnnotations;
using ACE.LMS.Core.Models.Security;
using System;
using System.Collections.Generic;
using ACE.LMS.Core.Models.Directory;

namespace ACE.LMS.Core.Models.Library
{
    public partial class Student : BaseEntity
    {

        public Student()
        {
            this.BookRequests = new List<BookRequest>();
            this.BookIssues = new List<BookIssue>();
        }
        //public int Id { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Enrollment No:")]
        public string EnrollmentNo { get; set; }

        [Required(ErrorMessage = "Please select Gender")]
        [Display(Name = "Gender:")]
        public int Gender { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Father's name is required")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Father's Name/Native:")]
        public string FatherName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mother's name is required")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Mother's Name/Native:")]
        public string MotherName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Father's native is required")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FatherNative { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mother's native is required")]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string MotherNative { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Present Address is required")]
        [Display(Name = "Present/Permanent Address:")]
        public string PresentAddress { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Permanent Address is required")]
        [Display(Name = "Permanent Address:")]
        public string PermanentAddress { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "City is required")]
        [Display(Name = "City:")]
        public int CityId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of Birth is required")]
        [Display(Name = "Date of Birth:")]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DOB { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        [Display(Name = "Mobile:")]
        public string Mobile { get; set; }

        [Display(Name = "Phone:")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email/AlternateEmail:")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "AlternateEmail is required")]
        [EmailAddress]
        [Display(Name = "AlternateEmail:")]
        public string AlternateEmail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "SSC Result is required")]
        [Display(Name = "10th board result/year:")]
        public string SSCResult { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Year is required")]
        [Display(Name = "10th board result/year:")]
        public Nullable<int> SSCPassingYear { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "HSC Result is required")]
        [Display(Name = "12th board result/year:")]
        public string HSCResult { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Year is required")]
        [Display(Name = "12th board result/year:")]
        public Nullable<int> HSCPassingYear { get; set; }

        public string ReferenceUserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Reference is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Reference Name/Number:")]
        public string OtherReference { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Father's occupation is required")]
        [Display(Name = "Father's occupation:")]
        public string FatherOccupation { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Family yearly income is required")]
        [Display(Name = "Family yearly income:")]
        public Nullable<System.Int32> FamilyYearlyIncome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "No of Brother & Sister is required")]
        [Display(Name = "No. of brothers/sisters in higher education:")]
        public Nullable<System.Int32> NoOfBrotherSis { get; set; }

        public Nullable<bool> IsLike { get; set; }
        public Nullable<bool> IsLikeContribution { get; set; }
        public Nullable<bool> IsLikeBookShare { get; set; }
        public Nullable<bool> IsLikeVoluntary { get; set; }

        [Display(Name = "Is Eligible?:")]
        public Nullable<bool> IsEligible { get; set; }

        [Display(Name = "Is Reference Validate?:")]
        public Nullable<bool> IsReferenceValidate { get; set; }

        [Display(Name = "Do you stay in hostel ?:")]
        public bool InHostel { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Photo is required")]
        [Display(Name = "Select:")]
        public string ImagePath { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Reference Mobileno. is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string ReferenceMobile { get; set; }
        public string Note { get; set; }
        public Nullable<int> Priority { get; set; }
        public virtual AspNetUser ReferenceBy { get; set; }
        public virtual AspNetUser CreatedByUser { get; set; }
        public virtual AspNetUser User { get; set; }
        public virtual AspNetUser UpdatedByUser { get; set; }
        public virtual ICollection<BookRequest> BookRequests { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<BookIssue> BookIssues { get; set; }

    }
}
