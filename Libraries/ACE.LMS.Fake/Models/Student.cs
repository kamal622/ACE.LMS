using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class Student
    {
        public Student()
        {
            this.BookRequests = new List<BookRequest>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string EnrollmentNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string FatherNative { get; set; }
        public string MotherNative { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public int CityId { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string SSCResult { get; set; }
        public Nullable<int> SSCPassingYear { get; set; }
        public string HSCResult { get; set; }
        public Nullable<int> HSCPassingYear { get; set; }
        public string ReferenceUserId { get; set; }
        public string OtherReference { get; set; }
        public string FatherOccupation { get; set; }
        public bool InHostel { get; set; }
        public string ImagePath { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string ReferenceMobile { get; set; }
        public Nullable<int> FamilyYearlyIncome { get; set; }
        public Nullable<int> NoOfBrotherSis { get; set; }
        public Nullable<bool> IsLike { get; set; }
        public Nullable<bool> IsLikeContribution { get; set; }
        public Nullable<bool> IsLikeBookShare { get; set; }
        public Nullable<bool> IsLikeVoluntary { get; set; }
        public Nullable<bool> IsEligible { get; set; }
        public Nullable<bool> IsReferenceValidate { get; set; }
        public string AlternateEmail { get; set; }
        public int Gender { get; set; }
        public string Note { get; set; }
        public Nullable<int> Priority { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual AspNetUser AspNetUser2 { get; set; }
        public virtual AspNetUser AspNetUser3 { get; set; }
        public virtual ICollection<BookRequest> BookRequests { get; set; }
        public virtual City City { get; set; }
    }
}
