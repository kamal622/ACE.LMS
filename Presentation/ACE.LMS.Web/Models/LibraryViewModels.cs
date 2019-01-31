using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACE.LMS.Core.Models.Library;
using System.ComponentModel.DataAnnotations;

namespace ACE.LMS.Web.Models
{
    public class LibraryViewModels
    {
    }

    public class BookRequestViewModel
    {
        public Student Student { get; set; }
        public College College { get; set; }
        public BookRequest BookRequest { get; set; }
        public Branch Branch { get; set; }
        public List<BookRequestDetailsModel> BookRequestDetails { get; set; }
    }

    public class BookRequestDetailsModel
    {
        public string Subject { get; set; }
        public string Title { get; set; }
        public string Publication { get; set; }
        public string Author { get; set; }
        public string BookId { get; set; }
        public int? Id { get; set; }
    }

    public class StudentSearchModel
    {
        public string FullName { get; set; }
        public DateTime? DOB { get; set; }

        public string bDate { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string EnrollmentNo { get; set; }

        public int StudentID { get; set; }

        public string StudentUserID { get; set; }

    }


    public class ReturnData
    {
        public string Message { get; set; }

        public string Data { get; set; }
    }

    public class JsonResponse
    {
        public MessageType MsgType { get; set; }
        public List<string> Messages { get; set; }
        public string RedirectUrl { get; set; }
    }

    public enum MessageType
    {
        Validations = 1,
        Success = 2,
        Warning = 3,
        Error = 4
    }

    public class TermsAndConditionViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string FullName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fater name is required")]
        public string FatherName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Degree/diploma is required")]
        public string Degree { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "College/School is required")]
        public string College { get; set; }
    }

    public class StudentFeedBackViewModel
    {
        [Required(ErrorMessage = "Please select yes or no")]
        [Display(Name = "Do you like idea of book bank ?")]
        public Nullable<bool> isLike { get; set; }

        [Required(ErrorMessage = "Please select yes or no")]
        [Display(Name = "Would you like to share/contribute /return your assistance, once you are employed after completion of education?")]
        public Nullable<bool> IsLikeContribution { get; set; }

        [Required(ErrorMessage = "Please select yes or no")]
        [Display(Name = "Would you like to share your books with others in college?")]
        public Nullable<bool> IsLikeBookShare { get; set; }

        [Required(ErrorMessage = "Please select yes or no")]
        [Display(Name = "Can we avail your voluntary services in this regards?")]
        public Nullable<bool> IsLikeVoluntary { get; set; }
    }

    public class TreeViewData
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }
    }

    public class BookMaintenance
    {
        public Book Book { get; set; }
        public LibraryBook LibraryBook { get; set; }
        public string CategoryIds { get; set; }
    }
}