using ACE.LMS.Core.Models.Library;
using System;
using System.Collections.Generic;

namespace ACE.LMS.Core.Models.Security
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            this.AspNetUserClaims = new List<AspNetUserClaim>();
            this.AspNetUserLogins = new List<AspNetUserLogin>();
            this.BookRequestDetails = new List<BookRequestDetail>();
            this.BookRequestHistories = new List<BookRequestHistory>();
           
            this.CreatedByBookRequests = new List<BookRequest>();
            this.UpdatedByBookRequests = new List<BookRequest>();
            this.Books = new List<Book>();
            this.LibraryBookHistories = new List<LibraryBookHistory>();
            
            this.ReferredStudents = new List<Student>();
            this.CreatedstStudents = new List<Student>();
            this.Students = new List<Student>();
            this.UpdatedstStudents = new List<Student>();
          
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
        public int UserProfile_Id { get; set; }
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<BookRequestDetail> BookRequestDetails { get; set; }
        public virtual ICollection<BookRequestHistory> BookRequestHistories { get; set; }
     
        public virtual ICollection<BookRequest> CreatedByBookRequests { get; set; }
        public virtual ICollection<BookRequest> UpdatedByBookRequests { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<LibraryBookHistory> LibraryBookHistories { get; set; }
       
        public virtual ICollection<Student> ReferredStudents { get; set; }
        public virtual ICollection<Student> CreatedstStudents { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Student> UpdatedstStudents { get; set; }
       
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }

    }
}
