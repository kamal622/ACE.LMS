using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using ACE.LMS.Core.Models.Library;
using ACE.LMS.Services.Library;
using ACE.LMS.Web.App_Constant;
using ACE.LMS.Web.Framework.Mvc;
using ACE.LMS.Web.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Mvc.Html;
using ACE.LMS.Core;


namespace ACE.LMS.Web.Controllers
{
    [Authorize]
    public class AdminController : ACE.LMS.Web.Infrastructure.BaseController
    {
        private readonly BookService _bookService;
        private readonly BookRequestService _bookRequestService;
        private readonly HistoryService _historyService;
        private readonly IssueBookService _issueBookService;
        private readonly StudentService _studentService;
        private readonly WebHelper _webHelper;
        private readonly LibraryBookService _libraryBookService;
        private readonly BookRequestDetailRepository _bookRequestDetailService;
        private readonly UserService _userService;
        private readonly NoticeBoardService _noticeBoardService;

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }

        }
        public AdminController(BookService bookService,
            BookRequestService bookRequestService,
            HistoryService historyService,
            IssueBookService issueBookService,
            StudentService studentService,
            WebHelper webHelper,
            LibraryBookService libraryBookService,
            BookRequestDetailRepository bookRequestDetailService,
            UserService userService,
            NoticeBoardService noticeBoardService
            )
        {
            this._bookService = bookService;
            this._bookRequestService = bookRequestService;
            this._historyService = historyService;
            this._issueBookService = issueBookService;
            this._studentService = studentService;
            this._webHelper = webHelper;
            this._libraryBookService = libraryBookService;
            this._bookRequestDetailService = bookRequestDetailService;
            this._userService = userService;
            this._noticeBoardService = noticeBoardService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LibraryBookDetails()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetAllBooks()
        {
            List<Book> books = this._bookService.GetAll();
            var finalData = from a in books
                            select new
                            {
                                Id = a.Id,
                                Title = a.Title,
                                Author = a.Author,
                                Subject = a.Subject,
                                Publication = a.Publication,
                                IsNewRequest = a.IsNewRequest,
                                ApprovedBooks = a.ApprovedBooks,
                                TotalBooks = a.TotalBooks,
                                NewApprovedBooks = a.NewApprovedBooks,
                                IssuedBooks = a.IssuedBooks,
                                TornBooks = a.TornBooks,
                                AvailableBooks = a.AvailableBooks

                            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult GetLibraryBooksById(int id)
        {
            List<LibraryBook> lBooks = this._bookRequestService.GetLibraryBooksById(id);
            var finalData = from a in lBooks
                            select new
                            {
                                Id = a.Id,
                                BookNo = a.BookNo,
                                ClassNo = a.ClassNo,
                                Price = a.Price,
                                PurchaseDate = a.PurchaseDate,
                                IsVerified = a.IsVerified,
                                bookId = a.BookId,
                                // Status = a.Status
                                Status = a.Status == 1 ? "Available" : a.Status == 2 ? "Issued" : "Torn"

                            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonNetResult VerifyLibraryBook(int LibraryBookId, bool value)
        {

            this._libraryBookService.verifyLibraryBook(LibraryBookId, !value);

            return JsonNet(new ReturnData { Message = "Success" }, JsonRequestBehavior.AllowGet);

            //return JsonNet(new ReturnData {Message = "Please fill all the required fields."},
            //    JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult changeLibraryBookStatus(int LibraryBookId, int status, int bookId, string oldStatus)
        {
            this._bookRequestService.ChangeLibraryBookStatus(LibraryBookId, status, User.Identity.GetUserId());

            if (status == (int)LibraryBookStatus.Issued)
            {
                if (oldStatus == LibraryBookStatus.Torn.ToString())
                    this._bookRequestService.ChangeStockForTornToAvailable(bookId);
                this._bookRequestService.ChangeStockForIssue(bookId, bookId,false);
            }
            else if (status == (int)LibraryBookStatus.Available)
            {
                if (oldStatus == LibraryBookStatus.Torn.ToString())
                    this._bookRequestService.ChangeStockForTornToAvailable(bookId);
                else
                    this._bookRequestService.ChangeStockForReturn(bookId);
            }
            else if (status == (int)LibraryBookStatus.Torn)
            {

                if (oldStatus == LibraryBookStatus.Issued.ToString())
                    this._bookRequestService.ChangeStockForReturn(bookId);
                this._bookRequestService.ChangeStockForTorn(bookId);
            }

            return JsonNet(new ReturnData { Message = "Success" }, JsonRequestBehavior.AllowGet);
            //change 
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EligibleStudents()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetEligibleStudents(string enrollmentNo)
        {
            List<Student> students = this._studentService.GetAllStudents(enrollmentNo);
            var finalData = from a in students
                            select new
                            {
                                Id = a.Id,
                                EnrollmentNo = a.EnrollmentNo,
                                StudentName = a.FirstName + " " + a.LastName,
                                Father = a.FatherName + "(" + a.FatherNative + ")",
                                Mother = a.MotherName + "(" + a.MotherNative + ")",
                                City = a.City.Name,
                                Mobile = a.Mobile,
                                Phone = a.Phone,
                                Email = a.Email,
                                Reference = a.OtherReference + "(" + a.ReferenceMobile + ")",
                                CreatedOn = a.CreatedOn,
                                IsEligible = (a.IsEligible == null) ? "NAN" : a.IsEligible.ToString(),
                                IsReferenceValidate = (a.IsReferenceValidate == null) ? "NAN" : a.IsReferenceValidate.ToString(),
                                gender = a.Gender
                            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ApprovedBooks()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult StudentProfile(int id)
        {
            var studentModel = this._studentService.GetStudentDetailById(id);
            return View(studentModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonNetResult> StudentProfile(Student student)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                if (!ModelState.IsValid)
                {
                    JsonResponse response = new JsonResponse { Messages = new List<string>(), MsgType = MessageType.Validations };
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                        foreach (ModelError error in modelState.Errors)
                            response.Messages.Add(error.ErrorMessage);
                    return JsonNet(response, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var existingStudent = this._studentService.GetStudentByUserId(student.UserId);

                    student.UpdatedBy = userId;
                    student.UpdatedOn = DateTime.Now;
                    this._studentService.Update(student); // Student can not update. Need to request admin if require any change

                    if (student.IsEligible != null && existingStudent.IsEligible != student.IsEligible)
                    {
                        if (student.IsEligible.Value)
                        {
                            // Send welcome email
                            var emailText = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/EmailTemplates/StudentEligibleEmail.html"));
                            emailText = emailText.Replace("{{BASE_URL}}", _webHelper.GetStoreHost(false));
                            await UserManager.SendEmailAsync(student.UserId, "Verification Complete", emailText);
                        }
                        else
                        {
                            // send rejection email
                            var emailText = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/EmailTemplates/StudentNotEligibleEmail.html"));
                            emailText = emailText.Replace("{{BASE_URL}}", _webHelper.GetStoreHost(false));
                            await UserManager.SendEmailAsync(student.UserId, "Verification Complete", emailText);
                        }
                    }

                    return JsonNet(new JsonResponse { MsgType = MessageType.Success, Messages = new List<string>(), RedirectUrl = Url.Action("EligibleStudents", "Admin", null, Request.Url.Scheme) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
                var messages = new List<string>();
                messages.Add(ex.Message + ex.InnerException ?? ex.InnerException.Message);
                return JsonNet(new JsonResponse { MsgType = MessageType.Error, Messages = messages }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult BookRequest()
        {
            //var statusBag = from LibraryBookStatus L in Enum.GetValues(typeof(LibraryBookStatus))
            //               select new { ID = (int)L, Name = L.ToString() };
            //ViewBag.statusBag = JsonNet(statusBag, JsonRequestBehavior.AllowGet);
            //    //new SelectList(statusBag, "ID", "Name");

            return View();
        }

        public ActionResult IssueBookWithoutRequest()
        {
            return View();
        }

        public JsonNetResult SearchStudent(string searchField, string searchString)
        {
            var student = _studentService.StudentSearch(searchField,  searchString);
            StudentSearchModel model = new StudentSearchModel();
            if (student != null)
            {
                model.EnrollmentNo = student.EnrollmentNo;
                model.FullName = student.FirstName + " " + student.LastName;
                model.DOB = student.DOB;
                model.bDate =(student.DOB==null)?"": student.DOB.Value.ToString("dd/MM/yyyy");
                model.Mobile = student.Mobile;
                model.Email = student.Email;
                model.StudentID = student.Id;
                model.StudentUserID = student.UserId;
                //return View(student);
                return JsonNet(model, JsonRequestBehavior.AllowGet);
            }
            else
                return JsonNet(model, JsonRequestBehavior.AllowGet);
           
        }
        public JsonNetResult GetRequestsForStudent(string StudentUserID,string task)
        {
            int status = 0;
            if (task == "IssuePending")
                status =(int) BookRequestStatus.Approved;
            var allData = from a in this._bookRequestService.GetRequestDetailsForStudent(StudentUserID,status)
                          orderby a.BookRequest.RequestDate descending
                          select new StudentViewModels.BookRequestDetailsData
                          {
                              Id = a.Id,
                              RequestId = a.BookRequest.Id,
                              BookId=a.BookId,
                              BookTitle = a.Book.Title,
                              BookAuthor = a.Book.Author,
                              BookPublisher = a.Book.Publication,
                              Subject = a.Subject,
                              AccessNo="",
                              RequestDate = a.BookRequest.RequestDate,
                              IssueDate = a.BookIssues.Select(s => s.IssueDate).FirstOrDefault(),
                              ReturnDate = a.BookIssues.Select(s => s.BookReturns.Select(s2 => s2.ReturnDate).FirstOrDefault()).FirstOrDefault(),
                              ReturnBefore = a.BookIssues.Select(s => s.ReturnOnOrBefore).FirstOrDefault(),
                              Status = a.Status == 1 ? "Pending" : a.Status == 2 ? "Approved" : a.Status == 3 ? "Rejected" : a.Status == 4 ? "Issued" : "Returned"

                          };

            return JsonNet(allData, JsonRequestBehavior.AllowGet);
        }

        public JsonNetResult GetIssuedBooksForStudent(string StudentUserID, string task)
        {
            int status = (int)BookRequestStatus.Issued;
            if (task == "Return")
                status = (int)BookRequestStatus.Returned;

            var allData = from a in this._issueBookService.GetIssuedBooksForStudent(StudentUserID,status)
                          orderby a.IssueDate descending
                          select new StudentViewModels.BookRequestDetailsData
                          {
                              Id = a.Id,
                              RequestId = 0,
                              BookRequestDetailsId=  a.BookRequestDetailsId ?? 0,
                              BookTitle = a.LibraryBook.Book.Title,
                              BookAuthor = a.LibraryBook.Book.Author,
                              BookPublisher = a.LibraryBook.Book.Publication,
                              Subject = "",
                              AccessNo=a.LibraryBook.BookNo,
                              RequestDate = a.IssueDate.Value,
                              IssueDate = a.IssueDate.Value,
                              ReturnDate = a.BookReturns.Select(s2 => s2.ReturnDate).FirstOrDefault(),
                              ReturnBefore = a.ReturnOnOrBefore,
                              Status = a.Status == 1 ? "Pending" : a.Status == 2 ? "Approved" : a.Status == 3 ? "Rejected" : a.Status == 4 ? "Issued" : "Returned",
                              returnStatus=(a.Status==5)?true:false
                          };

            return JsonNet(allData, JsonRequestBehavior.AllowGet);
        }


        //Old Functionality
        //[HttpPost]
        //public JsonNetResult IssueWithoutRequest( string BookNo,  BookRequest bookRequest, BookIssue bookIssue)
        //{
        //   LibraryBook librayBook = _libraryBookService.GetByAccessNo(BookNo);
        //    using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
        //    {
        //        //Insert BookRequest
        //        bookRequest.CreatedBy = User.Identity.GetUserId();
        //    bookRequest.CreatedOn = DateTime.Now;
        //    int BookRequestId = this._bookRequestService.InsertBookRequest(bookRequest);

        //    //Insert BookRequestDetail
        //    var bookRequestDetail = new BookRequestDetail
        //    {
        //        RequestId = BookRequestId,
        //        BookId = librayBook.BookId.Value,
        //        Status = (int)BookRequestStatus.Issued,
        //        Subject = "-",
        //        Notes = "CREATED BY SYSTEM",
        //        UpdatedBy = User.Identity.GetUserId(),
        //        UpdatedOn = DateTime.Now
        //    };

        //    int bookReqDetId = this._bookRequestService.InsertBookRequestDetail(bookRequestDetail);
        //    if (bookReqDetId >= 0)
        //    {
        //        this._historyService.InsertBookRequestHistory(new BookRequestHistory { BookRequestDetailId = bookReqDetId, Status = (int)BookRequestStatus.Pending, CreatedBy = User.Identity.GetUserId(), CreatedOn = DateTime.Now });
        //    }
        //    //Approve Request

        //    //Update status of bookRequestDetail
        //    // int books = this._bookRequestService.ChangeBookStatus(bookReqDetId, (int)BookRequestStatus.Approved, User.Identity.GetUserId(),"");

        //    //Update bookStock for Approved book
        //    int response2 = 0;

        //    response2 = this._bookRequestService.ChangeStockForApprove(librayBook.BookId.Value);

        //    if (response2 != 0)
        //    {
        //        var bkHistory = new BookRequestHistory
        //        {
        //            BookRequestDetailId = bookReqDetId,
        //            Status = (int)BookRequestStatus.Approved,
        //            CreatedBy = User.Identity.GetUserId(),
        //            CreatedOn = DateTime.Now
        //        };
        //        this._historyService.InsertBookRequestHistory(bkHistory);
        //    }
        //        //issue book
        //        bookIssue = new BookIssue
        //        {
        //            BookRequestDetailsId = bookReqDetId,
        //            Status = (int)BookRequestStatus.Issued,
        //            Notes = "CREATED BY SYSTEM"
        //        };

        //        //    scope.Complete();

        //        //}
        //        //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
        //        //{
        //        IssueBookFunction(bookIssue, BookNo, librayBook.BookId.Value);
        //        scope.Complete();

        //    }
        //    return JsonNet(new ReturnData { Message = "Success" }, JsonRequestBehavior.AllowGet);

        //}


        [HttpPost]
        public JsonNetResult IssueWithoutRequest(string BookNo, BookRequest bookRequest, BookIssue bookIssue)
        {
            var newBookId = this._bookRequestService.GetBookIdFromAccessNo(BookNo);

            if (newBookId != null)
            {
                if (_libraryBookService.getLibraryBookStatus(BookNo) == 1)
                {
                    LibraryBook librayBook = _libraryBookService.GetByAccessNo(BookNo);

                    using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
                    {

                        //issue book
                        bookIssue.BookRequestDetailsId = null;
                        bookIssue.Status = (int)BookRequestStatus.Issued;
                        // Notes = "Issued directly without request",
                        bookIssue.StudentId = bookRequest.StudentId;
                           
                        IssueBookFunction(bookIssue, BookNo, librayBook.BookId.Value,true);
                        scope.Complete();

                    }
                    return JsonNet(new ReturnData { Message = "Success" }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return JsonNet(new ReturnData { Message = "Check availability with system..This book is not available in database." }, JsonRequestBehavior.AllowGet);

                }
            }
            else
                return JsonNet(new ReturnData { Message = "Invalid Access No." }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult IssueBook()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetRequestDetails(int bookRequestId, string task)
        {
            List<newBookRequestDetail> alldata = null;
            if (task == "Approve")
                alldata = this._bookRequestService.GetRequestDetails(bookRequestId);
            else
                if (task == "View")
                alldata = this._bookRequestService.GetApprovedRequestDetails(bookRequestId,
                (int)BookRequestStatus.Approved);
            else
                alldata = this._bookRequestService.GetRequestDetailsForIssueReturn(bookRequestId,
                    (int)BookRequestStatus.Approved, (int)BookRequestStatus.Issued);


            //int i = 0;
            var finalData = from d in alldata
                            select new
                            {
                                BookRequestDetailId = d.BookRequestDetailId,
                                BookId = d.BookId,
                                Title = d.Title,
                                Author = d.Author,
                                Publication = d.Publication,
                                AvailableBooks = d.totalAvailable,
                                Status = d.Status,
                                totalInLibrary = d.totalInLibrary,
                                ApprovedBooks = d.ApprovedBooks,
                                NewApproval = d.NewApproval,
                                IssuedBooks = d.IssuedBooks,
                                TornBooks = d.TornBooks,
                                Available = d.Available,
                                IssueId = d.IssueId,
                                Issue = (d.Issue > 0),
                                Return = (d.Return > 0),
                                
                            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonNetResult GetPendingRequests(DateTime? startDate, DateTime? endDate, int eligible)
        {
            var allData = this._bookRequestService.GetPendingRequests(startDate, endDate, eligible);

            var finalData = from a in allData
                            select new
                            {
                                Id = a.Id,
                                EnrollmentNo = a.Student.EnrollmentNo,
                                StudentName = a.Student.FirstName + " " + a.Student.LastName,
                                CollegeName = a.College.Name + "(" + a.Branch.Name + ")",
                                BranchName = a.Branch.Name,
                                SemesterStartDate = a.SemesterStartDate,
                                SemesterEndDate = a.SemesterEndDate,
                                RequestDate = a.RequestDate,
                                Reference = a.Student.OtherReference + "(" + a.Student.ReferenceMobile + ")",
                                // EnrollmentNo = a.Student.EnrollmentNo,
                                IsEligible = (a.Student.IsEligible == null) ? "NAN" : a.Student.IsEligible.ToString(),
                                IsReferenceValidate = (a.Student.IsReferenceValidate == null) ? "NAN" : a.Student.IsReferenceValidate.ToString(),
                                gender = a.Student.Gender
                            };

            return JsonNet(finalData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult GetRequests(string task)
        {
            List<BookRequest> allData = null;
            if (task == "Approve")
                allData = this._bookRequestService.GetRequests();
            else if (task == "View")
                allData = this._bookRequestService.GetApprovedRequests((int)BookRequestStatus.Approved);
            else
                allData = this._bookRequestService.GetRequestsForIssue((int)BookRequestStatus.Approved, (int)BookRequestStatus.Issued);


            var finalData = from a in allData
                            select new
                            {
                                Id = a.Id,
                                EnrollmentNo = a.Student.EnrollmentNo,
                                StudentName = a.Student.FirstName + " " + a.Student.LastName,
                                CollegeName = a.College.Name + "(" + a.Branch.Name + ")",
                                BranchName = a.Branch.Name,
                                SemesterStartDate = a.SemesterStartDate,
                                SemesterEndDate = a.SemesterEndDate,
                                RequestDate = a.RequestDate,
                                Reference = a.Student.OtherReference + "(" + a.Student.ReferenceMobile + ")"
                            };

            return JsonNet(finalData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult ApproveBookRequest(int BookRequestDetailId, int status, int oldStatus)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
            {
                //Update status of bookRequestDetail
                int bookId = this._bookRequestService.ChangeBookStatus(BookRequestDetailId, status, User.Identity.GetUserId(),"");

                //Update bookStock for Approved book
                int response = 0;
                if (bookId != 0)
                {
                    if (status == (int)BookRequestStatus.Approved)
                        response = this._bookRequestService.ChangeStockForApprove(bookId);
                    else if (status == (int)BookRequestStatus.Rejected && oldStatus == (int)BookRequestStatus.Approved)
                        response = this._bookRequestService.ChanegeStockForReject(bookId);
                }
                if (response != 0)
                {
                    var bkHistory = new BookRequestHistory
                    {
                        BookRequestDetailId = BookRequestDetailId,
                        Status = status,
                        CreatedBy = User.Identity.GetUserId(),
                        CreatedOn = DateTime.Now
                    };
                    this._historyService.InsertBookRequestHistory(bkHistory);
                }

                if (status == 2)
                {
                    var existingBook = this._bookService.GetBookById(bookId);

                    var emailText = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/EmailTemplates/RequestApproved.html"));
                    emailText = emailText.Replace("{{BASE_URL}}", _webHelper.GetStoreHost(false));
                    emailText = emailText.Replace("{{BOOK_TITLE}}", existingBook.Title);
                    emailText = emailText.Replace("{{BOOK_AUTHOR}}", existingBook.Author);
                    emailText = emailText.Replace("{{BOOK_PUBLISHER}}", existingBook.Publication);
                    var studentUserId = this._bookRequestDetailService.GetStudentUserIdForBookRequestDetailId(BookRequestDetailId);
                    UserManager.SendEmail(studentUserId, "Book Issued", emailText);
                }
                scope.Complete();
            }


            return JsonNet(new ReturnData { Message = "Success" }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonNetResult AddOtherRequestDetail(string newBookId, string BookNo, int BookRequestId, int oldBookRequestDetailsId)
        {
            BookIssue bookIssue = null;
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
            {
                #region RejectOldRequestDetail
                //Update status of bookRequestDetail
                int bookId = this._bookRequestService.ChangeBookStatus(oldBookRequestDetailsId, (int)BookRequestStatus.Rejected, User.Identity.GetUserId(),"Rejected By System(As Request was wrong.Issued other request for BookNo "+BookNo.ToString());

                //Update bookStock for Rejected book
                int response = 0;
                if (bookId != 0)
                {
                    response = this._bookRequestService.ChanegeStockForReject(bookId);
                }
                if (response != 0)
                {
                    var bkHistory = new BookRequestHistory
                    {
                        BookRequestDetailId = oldBookRequestDetailsId,
                        Status = (int)BookRequestStatus.Rejected,
                        CreatedBy = User.Identity.GetUserId(),
                        CreatedOn = DateTime.Now
                    };
                    this._historyService.InsertBookRequestHistory(bkHistory);
                }

                #endregion

                #region AddNewRequestDetail
                //Add new request for new book accessno
                var bookRequestDetail = new BookRequestDetail
                {
                    RequestId = BookRequestId,
                    BookId = int.Parse(newBookId),
                    Status = (int)BookRequestStatus.Issued,
                    Subject = "-",
                    Notes = "CREATED BY SYSTEM",
                    UpdatedBy = User.Identity.GetUserId(),
                    UpdatedOn = DateTime.Now
                };

                int bookReqDetId = this._bookRequestService.InsertBookRequestDetail(bookRequestDetail);
                if (bookReqDetId >= 0)
                {
                    this._historyService.InsertBookRequestHistory(new BookRequestHistory { BookRequestDetailId = bookReqDetId, Status = (int)BookRequestStatus.Pending, CreatedBy = User.Identity.GetUserId(), CreatedOn = DateTime.Now });
                }

                //Approve Request

                //Update status of bookRequestDetail
               // int books = this._bookRequestService.ChangeBookStatus(bookReqDetId, (int)BookRequestStatus.Approved, User.Identity.GetUserId(),"");

                //Update bookStock for Approved book
                int response2 = 0;
               
                    response2 = this._bookRequestService.ChangeStockForApprove(bookId);

                if (response2 != 0)
                {
                    var bkHistory = new BookRequestHistory
                    {
                        BookRequestDetailId = bookReqDetId,
                        Status = (int)BookRequestStatus.Approved,
                        CreatedBy = User.Identity.GetUserId(),
                        CreatedOn = DateTime.Now
                    };
                    this._historyService.InsertBookRequestHistory(bkHistory);
                }

                //issue book
                int studentId = this._bookRequestDetailService.GetStudentIdForBookRequestDetailId(oldBookRequestDetailsId);
                 bookIssue = new BookIssue
                {
                    BookRequestDetailsId = bookReqDetId,
                    Status = (int)BookRequestStatus.Issued,
                    StudentId=studentId,
                    Notes = "CREATED BY SYSTEM"
                };

               #endregion
            //    scope.Complete();

            //}
            //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
            //{
                IssueBookFunction(bookIssue, BookNo, int.Parse(newBookId),false);
                scope.Complete();

            }
            return JsonNet(new ReturnData { Message = "Success" }, JsonRequestBehavior.AllowGet);

        }

        public int IssueBookFunction(BookIssue bookIssue, string BookNo, int newBookId,bool isDirectIssue)
        {
            int BookRequestId = 0;

            try
            {
                bookIssue.LibraryBooksId = this._bookRequestService.GetLibraryBookIdFromAccessNo(BookNo);
                bookIssue.IssueDate = DateTime.Now;
                bookIssue.IssuedBy = User.Identity.GetUserId();
                bookIssue.Status = (int)BookRequestStatus.Issued;
                this._issueBookService.InsertBookIssue(bookIssue);

                //Add Library book History 
                var lbkHistory = new LibraryBookHistory()
                {
                    LibraryBookId = bookIssue.LibraryBooksId,
                    Status = (int)LibraryBookStatus.Issued,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now

                };
                this._historyService.InsertLibraryBookHistory(lbkHistory);

                //Change Library book status
                if (bookIssue.LibraryBooksId != null)
                    this._bookRequestService.ChangeLibraryBookStatus(bookIssue.LibraryBooksId.Value,
                        (int)LibraryBookStatus.Issued, User.Identity.GetUserId());

                if(bookIssue.BookRequestDetailsId!=0 || bookIssue.BookRequestDetailsId != null)
                {
                    //Add Book request History
                    var bkHistory = new BookRequestHistory()
                    {
                        BookRequestDetailId = bookIssue.BookRequestDetailsId,
                        Status = (int)BookRequestStatus.Issued,
                        CreatedBy = User.Identity.GetUserId(),
                        CreatedOn = DateTime.Now

                    };
                    this._historyService.InsertBookRequestHistory(bkHistory);

                    //Change Book Request Status
                    BookRequestId = this._bookRequestService.getBookRequestIdFromDetail(bookIssue.BookRequestDetailsId.Value);
                    this._bookRequestService.ChangeBookStatus(bookIssue.BookRequestDetailsId.Value, (int)BookRequestStatus.Issued,
                           User.Identity.GetUserId(), "");
                }


                //Change Book Request Status
                //if (bookIssue.BookRequestDetailsId != null)
                //{
                //BookRequestId = this._bookRequestService.getBookRequestIdFromDetail(bookIssue.BookRequestDetailsId.Value);
                //this._bookRequestService.ChangeBookStatus(bookIssue.BookRequestDetailsId.Value, (int)BookRequestStatus.Issued,
                //       User.Identity.GetUserId(), "");

                //}

                //Change Book Stock
                this._bookRequestService.ChangeStockForIssue(newBookId, newBookId,isDirectIssue);

                var existingBook = this._bookService.GetBookById(newBookId);

                var emailText = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/EmailTemplates/BookIssued.html"));
                emailText = emailText.Replace("{{BASE_URL}}", _webHelper.GetStoreHost(false));
                emailText = emailText.Replace("{{BOOK_TITLE}}", existingBook.Title);
                emailText = emailText.Replace("{{BOOK_AUTHOR}}", existingBook.Author);
                emailText = emailText.Replace("{{BOOK_PUBLISHER}}", existingBook.Publication);
                emailText = emailText.Replace("{{BOOK_RECEIVER}}", bookIssue.BookReceivedBy);
                emailText = emailText.Replace("{{BOOK_RETURN_DATE}}", bookIssue.ReturnOnOrBefore.Value.ToShortDateString());
                var studentUserId = this._bookRequestDetailService.GetStudentUserIdForBookRequestDetailId(bookIssue.BookRequestDetailsId.Value);
                UserManager.SendEmail(studentUserId, "Book Issued", emailText);
            }
            catch (Exception ex)
            {

                return 0;
            }
            return BookRequestId;
        }

        [HttpPost]
        public JsonNetResult IssueBookToStudent(BookIssue bookIssue, string BookNo, int oldBookId)
        {
            if (string.IsNullOrEmpty(BookNo) || bookIssue.ReceiverNo == null || bookIssue.BookReceivedBy == null)
            {
                return JsonNet(new ReturnData { Message = "Please fill all the required fields." }, JsonRequestBehavior.AllowGet);

            }

            var newBookId = this._bookRequestService.GetBookIdFromAccessNo(BookNo);

            if (newBookId != null)
            {
                if (newBookId != oldBookId)
                    return JsonNet(new ReturnData { Message = "NewRequest", Data = newBookId.ToString() }, JsonRequestBehavior.AllowGet);

                if(_libraryBookService.getLibraryBookStatus(BookNo) ==1)
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
                    {
                        bookIssue.StudentId = this._bookRequestDetailService.GetStudentIdForBookRequestDetailId(bookIssue.BookRequestDetailsId.Value);
                        
                        int BookRequestId = IssueBookFunction(bookIssue, BookNo, newBookId.Value,false);
                        scope.Complete();
                        return JsonNet(new ReturnData { Message = "Success", Data = BookRequestId.ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                    return JsonNet(new ReturnData { Message = "Book is not available." }, JsonRequestBehavior.AllowGet);


            }
            else
                return JsonNet(new ReturnData { Message = "Invalid Access No." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult ReturnBook(int BookRequestDetailId, int BookId, BookReturn bookReturn,string AccessNo)
        {
            if (bookReturn.Mobile == null || bookReturn.SubmittedBy == null)
                return JsonNet(new ReturnData { Message = "Please fill all the required fields." }, JsonRequestBehavior.AllowGet);


            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
            {
                //Insert Book Return

                bookReturn.ReturnDate = DateTime.Now;
                bookReturn.UpdatedBy = User.Identity.GetUserId();
                bookReturn.UpdatedOn = DateTime.Now;

                this._issueBookService.InsertBookReturn(bookReturn);

                var libraryBookIdfromIssue = this._issueBookService.GetLibraryBookIdfromIssue(bookReturn.BookIssueId.Value);
                int libraryBookId = 0;
                if (libraryBookIdfromIssue != null)
                {
                    libraryBookId = libraryBookIdfromIssue.Value;
                }
                //Add Library book History 
                var lbkHistory = new LibraryBookHistory()
                {
                    LibraryBookId = libraryBookId,
                    Status = (int)LibraryBookStatus.Available,
                    CreatedBy = User.Identity.GetUserId(),
                    CreatedOn = DateTime.Now

                };
                this._historyService.InsertLibraryBookHistory(lbkHistory);

                //Change Library book status
                this._bookRequestService.ChangeLibraryBookStatus(libraryBookId,
                        (int)LibraryBookStatus.Available, User.Identity.GetUserId());
                //Change BookIssue status
                this._issueBookService.changeIssueStatus(bookReturn.BookIssueId.Value, (int)BookRequestStatus.Returned, User.Identity.GetUserId());

                if(BookRequestDetailId!=0)
                {
                    //Add Book request History
                    var bkHistory = new BookRequestHistory()
                    {
                        BookRequestDetailId = BookRequestDetailId,
                        Status = (int)BookRequestStatus.Returned,
                        CreatedBy = User.Identity.GetUserId(),
                        CreatedOn = DateTime.Now

                    };
                    this._historyService.InsertBookRequestHistory(bkHistory);

                    //Change Book Request Status
                    String bookRequestId = this._bookRequestService.getBookRequestIdFromDetail(BookRequestDetailId).ToString();

                    this._bookRequestService.ChangeBookStatus(BookRequestDetailId, (int)BookRequestStatus.Returned,
                         User.Identity.GetUserId(), "");

                    
                }
                if(BookId==0)
                {
                    //Get BookId
                    BookId = _libraryBookService.GetByAccessNo(AccessNo).BookId.Value;

                }

                //Change Book Stock

                this._bookRequestService.ChangeStockForReturn(BookId);


                var existingBook = this._bookService.GetBookById(BookId);



                var emailText = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/EmailTemplates/BookReceived.html"));
                emailText = emailText.Replace("{{BASE_URL}}", _webHelper.GetStoreHost(false));
                emailText = emailText.Replace("{{BOOK_TITLE}}", existingBook.Title);
                emailText = emailText.Replace("{{BOOK_AUTHOR}}", existingBook.Author);
                emailText = emailText.Replace("{{BOOK_PUBLISHER}}", existingBook.Publication);

                var studentUserId="";
                if (BookRequestDetailId != 0)
                    studentUserId = this._bookRequestDetailService.GetStudentUserIdForBookRequestDetailId(BookRequestDetailId);
                else
                    studentUserId = this._issueBookService.GetStudentUserIdForIssuelId(bookReturn.BookIssueId.Value);
                UserManager.SendEmail(studentUserId, "Book Returned", emailText);

                scope.Complete();
                return JsonNet(new ReturnData { Message = "Success", Data = BookId.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }

        //public JsonNetResult ApproveBook(BookRequestDetail detail, int bookRequestDetailId)
        //{
        //    try
        //    {
        //        if (detail != null)
        //        {
        //            //Check available list if book is approved and assign one book
        //            // int LibraryBookId = 0;
        //            if (detail.Status == (int)BookRequestStatus.Approved)
        //            {
        //                int libraryBookId = this._bookRequestService.GetAvailableBook(detail.BookId,
        //             (int)LibraryBookStatus.Available);
        //                if(libraryBookId==0)
        //                    return JsonNet(new ReturnData { Message = "No book available in Library." }, JsonRequestBehavior.AllowGet);

        //                detail.BookAssigned = libraryBookId;

        //                //change the status of Library book
        //                this._bookRequestService.ChangeStatus(libraryBookId, (int)LibraryBookStatus.Approved, User.Identity.GetUserId());

        //                //Add Library Book History
        //                var lbHistory = new LibraryBookHistory
        //                {
        //                    LibraryBookId = libraryBookId,
        //                    Status = (int)LibraryBookStatus.Approved,
        //                    CreatedBy = User.Identity.GetUserId(),
        //                    CreatedOn = DateTime.Now
        //                };
        //                this._historyService.InsertLibraryBookHistory(lbHistory);

        //            }
        //            detail.UpdatedBy = User.Identity.GetUserId();
        //            detail.UpdatedOn = DateTime.Now;
        //          this._bookRequestService.ApproveBook(detail,bookRequestDetailId);
        //            //Add BookRequest History
        //          if (bookRequestDetailId != 0)
        //            {
        //                var bkHistory = new BookRequestHistory
        //                {
        //                    BookRequestDetailId = bookRequestDetailId,
        //                    Status = detail.Status,
        //                    CreatedBy = User.Identity.GetUserId(),
        //                    CreatedOn = DateTime.Now
        //                };

        //                this._historyService.InsertBookRequestHistory(bkHistory);
        //            }


        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return JsonNet(new ReturnData { Message = "Success" }, JsonRequestBehavior.AllowGet);

        //}

        #region UserMaintenance
        [Authorize(Roles = "Admin")]
        public ActionResult UserMaintenance()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetUserList(string userType, int emailConfirmed, int blocked, int active, string firstName, string lastName, string email)
        {
            var userData = this._userService.GetUserList(userType, emailConfirmed, blocked, active, firstName, lastName, email);
            return JsonNet(userData, JsonRequestBehavior.AllowGet);
        }

        public JsonNetResult UpdateUserData(string userId, int userProfileId, bool isEmailConfirmed, bool isActive, bool isBlocked)
        {
            try
            {
                this._userService.UpdateUserData(userId, userProfileId, isEmailConfirmed, isActive, isBlocked);
                return JsonNet(new ReturnData { Message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return JsonNet(new ReturnData { Message = "Error", Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonNetResult> VerifyEmail(string userId)
        {
            try
            {
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(userId);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userId, code = code }, protocol: Request.Url.Scheme);
                var emailText = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/EmailTemplates/WelcomeEmail.html"));
                emailText = emailText.Replace("{{BASE_URL}}", _webHelper.GetStoreHost(false));
                emailText = emailText.Replace("{{CALLBACK_URL}}", callbackUrl);
                await UserManager.SendEmailAsync(userId, "Account Activation Details", emailText);
                return JsonNet(new ReturnData { Message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return JsonNet(new ReturnData { Message = "Error", Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Noticeboard

        [Authorize(Roles = "Admin")]
        public ActionResult Noticeboard()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult GetAllNotice()
        {
            var noticeBoardData = this._noticeBoardService.GetAllNotice();
            return JsonNet(noticeBoardData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult SaveNotice(NoticeBoard noticeBoard)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    JsonResponse response = new JsonResponse { Messages = new List<string>(), MsgType = MessageType.Validations };
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                        foreach (ModelError error in modelState.Errors)
                            response.Messages.Add(error.ErrorMessage);
                    return JsonNet(response, JsonRequestBehavior.AllowGet);
                }
                string userId = User.Identity.GetUserId();
                noticeBoard.CreatedBy = userId;
                noticeBoard.CreatedDate = DateTime.Now;
                this._noticeBoardService.SaveNotice(noticeBoard);
                return JsonNet(new JsonResponse { MsgType = MessageType.Success, Messages = new List<string>() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
                var messages = new List<string>();
                messages.Add(ex.Message + ex.InnerException ?? ex.InnerException.Message);
                return JsonNet(new JsonResponse { MsgType = MessageType.Error, Messages = messages }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonNetResult ActiveInactiveNotice(int noticeId, bool IsActive)
        {
            try
            {
                this._noticeBoardService.ActiveInactiveNotice(noticeId, IsActive);
                return JsonNet(new JsonResponse { MsgType = MessageType.Success, Messages = new List<string>() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
                var messages = new List<string>();
                messages.Add(ex.Message + ex.InnerException ?? ex.InnerException.Message);
                return JsonNet(new JsonResponse { MsgType = MessageType.Error, Messages = messages }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region BookMaintenance

        [Authorize(Roles = "Admin")]
        public ActionResult BookMaintenance()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveBook(Book book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    JsonResponse response = new JsonResponse { Messages = new List<string>(), MsgType = MessageType.Validations };
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                        foreach (ModelError error in modelState.Errors)
                            response.Messages.Add(error.ErrorMessage);
                    return JsonNet(response, JsonRequestBehavior.AllowGet);
                }

                return JsonNet(new JsonResponse { MsgType = MessageType.Success, Messages = new List<string>() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
                var messages = new List<string>();
                messages.Add(ex.Message + ex.InnerException ?? ex.InnerException.Message);
                return JsonNet(new JsonResponse { MsgType = MessageType.Error, Messages = messages }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
