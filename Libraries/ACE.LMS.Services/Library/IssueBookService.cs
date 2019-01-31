using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;

namespace ACE.LMS.Services.Library
{
    public class IssueBookService
    {

        private readonly IRepository<BookIssue> _bookIssueRepository;
        private readonly IRepository<BookReturn> _bookReturnRepository;

        public IssueBookService(IRepository<BookIssue> bookIssueRepository, IRepository<BookReturn> bookReturnRepository)
        {
            this._bookIssueRepository = bookIssueRepository;
            this._bookReturnRepository = bookReturnRepository;
        }
        public int InsertBookIssue(BookIssue bIssue)
        {
            this._bookIssueRepository.Insert(bIssue);
            return bIssue.Id;
        }

        public int InsertBookReturn(BookReturn bookReturn)
        {
            this._bookReturnRepository.Insert(bookReturn);
            return bookReturn.Id;
        }

        public int? GetLibraryBookIdfromIssue(int issueId)
        {
            var firstOrDefault = this._bookIssueRepository.Table.FirstOrDefault(w => w.Id == issueId);
            if (firstOrDefault != null)
                return firstOrDefault.LibraryBooksId;
            else
            {
                return null;
            }
        }

        public IQueryable<BookIssue> GetIssuedBooksForStudent(string studentUserId, int status)
        {
            return this._bookIssueRepository.Table.Where(w => w.Student.UserId == studentUserId).Where(w => w.Status == status);
        }
        public IQueryable<BookReturn> GetReturnedBooksForStudent(string studentUserId, int status)
        {
           return this._bookReturnRepository.Table.Where(w => w.BookIssue.Student.UserId == studentUserId);
          
        }

        public void changeIssueStatus(int bookIssueId, int status, string user)
        {
            BookIssue issue = this._bookIssueRepository.Table.FirstOrDefault(w => w.Id == bookIssueId);
            if (issue != null)
            {
                issue.Status = status;
                issue.UpdatedBy = user;
                issue.UpdatedOn = DateTime.Now;
                this._bookIssueRepository.Update(issue);

            }
        }

        public string GetStudentUserIdForIssuelId(int issueId)
        {
            return this._bookIssueRepository.Table.Where(w => w.Id == issueId).Select(s => s.Student.UserId).FirstOrDefault();
                //Select(f => f.BookRequest.Student.UserId).FirstOrDefault();

        }
    }
}