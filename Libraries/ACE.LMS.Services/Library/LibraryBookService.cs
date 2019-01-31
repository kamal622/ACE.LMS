using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.LMS.Services.Library
{
    public class LibraryBookService
    {
        private readonly IRepository<LibraryBook> _libraryBookRepository;
        public LibraryBookService(IRepository<LibraryBook> libraryBookRepository)
        {
            this._libraryBookRepository = libraryBookRepository;
        }

        public LibraryBook GetById(int Id)
        {
            return this._libraryBookRepository.GetById(Id);
        }

        public LibraryBook GetByAccessNo(string accessNo)
        {
            return this._libraryBookRepository.Table.FirstOrDefault(w => w.BookNo == accessNo);
        }

        public int verifyLibraryBook(int libraryBookId, bool value)
        {
            LibraryBook book = this._libraryBookRepository.Table.FirstOrDefault(w => w.Id == libraryBookId);
            if (book != null)
            {
                book.IsVerified = value;
                this._libraryBookRepository.Update(book);
                return 1;
            }
            return 0;
        }

        public List<LibraryBook> GetBookDetails(int bookId)
        {
            return this._libraryBookRepository.Table.Where(w => w.BookId == bookId).ToList();
        }

        public int Insert(LibraryBook libraryBook)
        {
            this._libraryBookRepository.Insert(libraryBook);
            return libraryBook.Id;
        }

        public void Update(LibraryBook libraryBook)
        {
            this._libraryBookRepository.Update(libraryBook);
        }

        public int getLibraryBookStatus(string bookno)
        {
           return this._libraryBookRepository.Table.Where(w => w.BookNo == bookno).FirstOrDefault().Status.Value;

        }
    }
}
