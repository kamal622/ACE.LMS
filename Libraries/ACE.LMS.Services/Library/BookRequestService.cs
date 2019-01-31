using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;

namespace ACE.LMS.Services.Library
{
    public class newBookRequestDetail
    {
        public int RequestId { get; set; }
        public int BookId { get; set; }

        public int BookRequestDetailId { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publication { get; set; }
        public int totalAvailable { get; set; }
        public int totalInLibrary { get; set; }

        public int ApprovedBooks { get; set; }

        public int NewApproval { get; set; }

        public int IssuedBooks { get; set; }

        public int TornBooks { get; set; }

        public string Available { get; set; }

        public int Issue { get; set; }
        public int Return { get; set; }
        public int? IssueId { get; set; }
    }

    public class BookRequestService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BookRequest> _bookRequestRepository;
        private readonly IRepository<BookRequestDetail> _bookRequestDetailRepository;
        private readonly IRepository<LibraryBook> _libraryBookRepository;

        public BookRequestService(IRepository<Book> bookRepository, IRepository<BookRequest> bookRequestRepository, IRepository<BookRequestDetail> bookRequestDetailRepository, IRepository<LibraryBook> libraryBookRepository)
        {
            this._bookRepository = bookRepository;
            this._bookRequestRepository = bookRequestRepository;
            this._bookRequestDetailRepository = bookRequestDetailRepository;
            this._libraryBookRepository = libraryBookRepository;
        }

        public List<LibraryBook> GetLibraryBooksById(int bookId)
        {
            return this._libraryBookRepository.Table.Where(w => w.BookId == bookId).ToList();
        }

        public int? GetBookIdFromAccessNo(string accessNo)
        {
            return this._libraryBookRepository.Table.Where(w => w.BookNo == accessNo).Select(s => s.BookId).FirstOrDefault();
        }

        public int? GetLibraryBookIdFromAccessNo(string accessNo)
        {
            return this._libraryBookRepository.Table.Where(w => w.BookNo == accessNo).Select(s => s.Id).FirstOrDefault();
        }
        public int ChangeStockForApprove(int bookId)
        {
            Book book = this._bookRepository.Table.FirstOrDefault(w => w.Id == bookId);
            if (book != null)
            {
                if (book.ApprovedBooks < book.AvailableBooks.Value)
                    book.ApprovedBooks = book.ApprovedBooks + 1;
                else
                    book.NewApprovedBooks = book.NewApprovedBooks + 1;
                this._bookRepository.Update(book);
                return 1;
            }
            return 0;
        }
        public int ChangeStockForReturn(int bookId)
        {
            Book book = this._bookRepository.Table.FirstOrDefault(w => w.Id == bookId);
            if (book != null)
            {
                book.IssuedBooks = book.IssuedBooks - 1;
                book.AvailableBooks = book.AvailableBooks + 1;
                this._bookRepository.Update(book);
                return 1;
            }
            return 0;
        }
        public int ChangeStockForTornToAvailable(int bookId)
        {
            Book book = this._bookRepository.Table.FirstOrDefault(w => w.Id == bookId);
            if (book != null)
            {
                book.TornBooks = book.TornBooks - 1;
                book.AvailableBooks = book.AvailableBooks + 1;
                this._bookRepository.Update(book);
                return 1;
            }
            return 0;
        }
        public int ChangeStockForTorn(int bookId)
        {
            Book book = this._bookRepository.Table.FirstOrDefault(w => w.Id == bookId);
            if (book != null)
            {
                book.AvailableBooks = book.AvailableBooks - 1;
                book.TornBooks = book.TornBooks + 1;
                this._bookRepository.Update(book);
                return 1;
            }
            return 0;
        }

        public int ChanegeStockForReject(int bookId)
        {
            Book book = this._bookRepository.Table.FirstOrDefault(w => w.Id == bookId);
            if (book != null)
            {
                if (book.NewApprovedBooks > 0)
                    book.NewApprovedBooks = book.NewApprovedBooks - 1;
                else if (book.ApprovedBooks > 0)
                    book.ApprovedBooks = book.ApprovedBooks - 1;
                this._bookRepository.Update(book);
                return 1;
            }
            return 0;
        }

        public int ChangeStockForIssue(int oldBookId, int newBookId, bool isDirectIssue)
        {

            Book oldBook = this._bookRepository.Table.FirstOrDefault(w => w.Id == oldBookId);

            if (oldBook != null)
            {
                if (oldBook.AvailableBooks <= 0)
                    return -1;
                if(!isDirectIssue)
                {
                    if (oldBook.ApprovedBooks > 0)
                        oldBook.ApprovedBooks -= 1;
                    else
                   if (oldBook.NewApprovedBooks > 0)
                        oldBook.NewApprovedBooks -= 1;

                }

                if (oldBookId == newBookId)
                {
                    oldBook.IssuedBooks += 1;
                    oldBook.AvailableBooks -= 1;
                }
                //else
                //{
                //    Book newBook = this._bookRepository.Table.FirstOrDefault(w => w.Id == newBookId);
                //    if (newBook != null)
                //    {
                //        newBook.IssuedBooks += 1;
                //        newBook.AvailableBooks -= 1;
                //        this._bookRepository.Update(newBook);
                //    }
                //}

                this._bookRepository.Update(oldBook);

                return 1;
            }

            return 0;
        }


        public int ChangeBookStatus(int bookrequestDetailId, int status, string userID,string note)
        {
            BookRequestDetail detail = this._bookRequestDetailRepository.Table.FirstOrDefault(w => w.Id == bookrequestDetailId);
            if (detail != null)
            {
                detail.Status = status;
                detail.UpdatedBy = userID;
                detail.UpdatedOn = DateTime.Now;
                detail.Notes = note;
                this._bookRequestDetailRepository.Update(detail);
                return detail.BookId;
            }
            else
            {
                return 0;
            }

        }

        public int getBookRequestIdFromDetail(int bookRequestDetailId)
        {
            return
                this._bookRequestDetailRepository.Table.Where(w => w.Id == bookRequestDetailId)
                    .FirstOrDefault()
                    .RequestId;
        }

        public int GetAvailableBook(int bookId, int status)
        {
            var library = this._libraryBookRepository.Table.Where(w => w.BookId == bookId)
                .Where(w => w.Status == status).FirstOrDefault(w => w.RemovedBy == null);
            if (library != null)
                return library.Id;
            else
                return 0;
        }

        public int ChangeLibraryBookStatus(int LibraryBookId, int status, string user)
        {
            LibraryBook lBook = this._libraryBookRepository.Table.FirstOrDefault(w => w.Id == LibraryBookId);
            if (lBook != null)
            {
                lBook.Status = status;
                lBook.UpdatedBy = user;
                lBook.UpdatedOn = DateTime.Now;
                this._libraryBookRepository.Update(lBook);
                return lBook.Status.Value;
            }
            else
            {

                return 0;
            }
        }

        public int InsertBookRequest(BookRequest bRequest)
        {
            this._bookRequestRepository.Insert(bRequest);
            return bRequest.Id;
        }

        public int InsertBookRequestDetail(BookRequestDetail bRequestDetail)
        {
            this._bookRequestDetailRepository.Insert(bRequestDetail);
            return bRequestDetail.Id;
        }

        public List<BookRequest> GetPendingRequests(DateTime? startDate, DateTime? endDate, int eligible)
        {
            if (endDate != null)
            {
                endDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59);

                return this._bookRequestRepository.Table.Where(w => w.RequestDate >= (startDate ?? w.RequestDate)
                    && w.RequestDate <= (endDate ?? w.RequestDate)
                    && w.Student.IsEligible == (eligible == 1 ? w.Student.IsEligible : eligible == 2 ? true : false)).OrderByDescending(o => o.RequestDate).ToList();
            }
            else
                return this._bookRequestRepository.Table.Where(w=> w.Student.IsEligible == (eligible == 1 ? w.Student.IsEligible : eligible == 2 ? true : false)).OrderByDescending(o => o.RequestDate).ToList();

        }

        public List<BookRequest> GetRequests()
        {
            return new List<BookRequest>(this._bookRequestRepository.Table.ToList().Where(w => w.Student.IsEligible == true).Where(w => w.Student.IsReferenceValidate == true));
        }

        public List<BookRequest> GetRequestsForIssue(int approve, int issue)
        {
            return (from a in this._bookRequestRepository.Table
                    join b in this._bookRequestDetailRepository.Table
                            on a.Id equals b.RequestId
                    where b.Status == issue || b.Status == approve
                    select a).Distinct().ToList();
        }
        public List<BookRequest> GetApprovedRequests(int approve)
        {
            return (from a in this._bookRequestRepository.Table
                    join b in this._bookRequestDetailRepository.Table
                            on a.Id equals b.RequestId
                    where b.Status == approve
                    select a).Distinct().ToList();
        }

       
        public List<newBookRequestDetail> GetRequestDetailsForIssueReturn(int BookRequestId, int approved, int issued)
        {
            var reqlist = (from req in this._bookRequestDetailRepository.Table
                           where req.RequestId == BookRequestId && (req.Status == approved || req.Status == issued)
                           select new newBookRequestDetail
                                      {
                                          RequestId = req.RequestId,
                                          BookId = req.BookId,
                                          BookRequestDetailId = req.Id,
                                          Status = req.Status,
                                          Title = req.Book.Title,
                                          Author = req.Book.Author,
                                          Publication = req.Book.Publication,
                                          totalInLibrary = req.Book.TotalBooks.Value,
                                          ApprovedBooks = req.Book.ApprovedBooks.Value,
                                          NewApproval = req.Book.NewApprovedBooks.Value,
                                          IssuedBooks = req.Book.IssuedBooks.Value,
                                          TornBooks = req.Book.TornBooks.Value,
                                          totalAvailable = req.Book.AvailableBooks.Value,
                                          Available = req.Book.AvailableBooks.ToString() + "/" + req.Book.TotalBooks.ToString(),
                                          IssueId = req.BookIssues.FirstOrDefault().Id,
                                          Issue = req.BookIssues.Count(),
                                          Return = req.BookIssues.FirstOrDefault().BookReturns.Count()
                                      }).ToList();
            return reqlist;
        }
        public List<newBookRequestDetail> GetApprovedRequestDetails(int BookRequestId, int approved)
        {
            var reqlist = (from req in this._bookRequestDetailRepository.Table
                           where req.RequestId == BookRequestId && (req.Status == approved)
                           select new newBookRequestDetail
                           {
                               RequestId = req.RequestId,
                               BookId = req.BookId,
                               BookRequestDetailId = req.Id,
                               Status = req.Status,
                               Title = req.Book.Title,
                               Author = req.Book.Author,
                               Publication = req.Book.Publication,
                               totalInLibrary = req.Book.TotalBooks.Value,
                               ApprovedBooks = req.Book.ApprovedBooks.Value,
                               NewApproval = req.Book.NewApprovedBooks.Value,
                               IssuedBooks = req.Book.IssuedBooks.Value,
                               TornBooks = req.Book.TornBooks.Value,
                               totalAvailable = req.Book.AvailableBooks.Value,
                               Available = req.Book.AvailableBooks.ToString() + "/" + req.Book.TotalBooks.ToString(),
                               IssueId = req.BookIssues.FirstOrDefault().Id,
                               Issue = req.BookIssues.Count(),
                               Return = req.BookIssues.FirstOrDefault().BookReturns.Count()
                           }).ToList();
            return reqlist;
        }
        public List<newBookRequestDetail> GetRequestDetails(int BookRequestId)
        {

            var reqlist = (from req in this._bookRequestDetailRepository.Table
                           where req.RequestId == BookRequestId
                           select new newBookRequestDetail
                                      {
                                          RequestId = req.RequestId,
                                          BookId = req.BookId,
                                          BookRequestDetailId = req.Id,
                                          Status = req.Status,
                                          Title = req.Book.Title,
                                          Author = req.Book.Author,
                                          Publication = req.Book.Publication,
                                          totalInLibrary = req.Book.TotalBooks.Value,
                                          ApprovedBooks = req.Book.ApprovedBooks.Value,
                                          NewApproval = req.Book.NewApprovedBooks.Value,
                                          IssuedBooks = req.Book.IssuedBooks.Value,
                                          TornBooks = req.Book.TornBooks.Value,
                                          totalAvailable = req.Book.AvailableBooks.Value,
                                          Available = req.Book.AvailableBooks.ToString() + "/" + req.Book.TotalBooks.ToString(),
                                          IssueId = req.BookIssues.FirstOrDefault().Id,
                                          Issue = req.BookIssues.Count(),
                                          Return = req.BookIssues.FirstOrDefault().BookReturns.Count()
                                      }).ToList();
            return reqlist;
        }
        public IQueryable<BookRequestDetail> GetRequestDetailsForStudent(string studentUserId)
        {
            return this._bookRequestDetailRepository.Table.Where(w => w.BookRequest.Student.UserId == studentUserId);
        }

        public IQueryable<BookRequestDetail> GetRequestDetailsForStudent(string studentUserId,int status)
        {
            return this._bookRequestDetailRepository.Table.Where(w => w.BookRequest.Student.UserId == studentUserId).Where(w=>w.Status==status);
        }

    }

}
