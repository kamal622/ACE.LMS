using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.LMS.Core.Models.Library
{
    public partial class LibraryBook : BaseEntity
    {
        public LibraryBook()
        {
            this.BookIssues = new List<BookIssue>();
            this.BookRequestDetails = new List<BookRequestDetail>();
            this.LibraryBookHistories = new List<LibraryBookHistory>();
        }

        // public int Id { get; set; }
        public Nullable<int> BookId { get; set; }

        [Display(Name = "Price:")]
        public Nullable<decimal> Price { get; set; }

        [Display(Name = "Date:")]
        public Nullable<System.DateTime> PurchaseDate { get; set; }

        [Display(Name = "Year:")]
        public Nullable<int> PublishedYear { get; set; }
        public Nullable<int> Status { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Accession No is required")]
        [Display(Name = "Acc#:")]
        public string BookNo { get; set; }

        [Display(Name = "Class#:")]
        public Nullable<int> ClassNo { get; set; }
        public string AddedBY { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string RemovedBy { get; set; }
        public Nullable<System.DateTime> RemovedOn { get; set; }
        public string ReasonForRemoving { get; set; }
        public string Notes { get; set; }
        public bool HasCD { get; set; }
        public string AccessionNo { get; set; }
        public bool IsVerified { get; set; }

        [Display(Name = "Pages:")]
        public Nullable<System.Int32> Pages { get; set; }

        [Display(Name = "ISBN:")]
        public string ISBN { get; set; }

        [Display(Name = "Store:")]
        public string Store { get; set; }
        public virtual ICollection<BookIssue> BookIssues { get; set; }
        public virtual ICollection<BookRequestDetail> BookRequestDetails { get; set; }
        public virtual Book Book { get; set; }

        public virtual ICollection<LibraryBookHistory> LibraryBookHistories { get; set; }

    }
}
