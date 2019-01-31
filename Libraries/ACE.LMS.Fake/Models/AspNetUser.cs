using System;
using System.Collections.Generic;

namespace ACE.LMS.Fake.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            this.AspNetUserClaims = new List<AspNetUserClaim>();
            this.AspNetUserLogins = new List<AspNetUserLogin>();
            this.BookRequestDetails = new List<BookRequestDetail>();
            this.BookRequestHistories = new List<BookRequestHistory>();
            this.BookRequests = new List<BookRequest>();
            this.BookRequests1 = new List<BookRequest>();
            this.Books = new List<Book>();
            this.LibraryBookHistories = new List<LibraryBookHistory>();
            this.Students = new List<Student>();
            this.Students1 = new List<Student>();
            this.Students2 = new List<Student>();
            this.Students3 = new List<Student>();
            this.AspNetRoles = new List<AspNetRole>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public Nullable<int> UserProfile_Id { get; set; }
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<BookRequestDetail> BookRequestDetails { get; set; }
        public virtual ICollection<BookRequestHistory> BookRequestHistories { get; set; }
        public virtual ICollection<BookRequest> BookRequests { get; set; }
        public virtual ICollection<BookRequest> BookRequests1 { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<LibraryBookHistory> LibraryBookHistories { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Student> Students1 { get; set; }
        public virtual ICollection<Student> Students2 { get; set; }
        public virtual ICollection<Student> Students3 { get; set; }
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
