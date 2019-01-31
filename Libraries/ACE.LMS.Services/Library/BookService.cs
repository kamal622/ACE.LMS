using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;
using System.Collections.Generic;
using System.Linq;

namespace ACE.LMS.Services.Library
{
    public class BookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        public List<Book> GetAll()
        {
            return this._bookRepository.Table.ToList();
        }

        public List<Book> GetBooks(int categoryId, string[] searchKeywords)
        {
            var allData = this._bookRepository.Table;

            if (categoryId > 1)
                allData = allData.Where(w => w.BookCategories.Select(s => s.CategoryId).Contains(categoryId));

            if (searchKeywords.Length > 0)
                allData = allData.Where(w => searchKeywords.Any(a => w.Title.Contains(a)) || searchKeywords.Any(a => w.Author.Contains(a)) || searchKeywords.Any(a => w.Publication.Contains(a))); // searchKeywords.Contains(w.Title) || 

            //allData = allData.Where(w => searchKeywords.Contains(w.Title) || searchKeywords.Contains(w.Author) || searchKeywords.Contains(w.Publication));

            return allData.ToList();
        }

        public Book GetBookById(int ID)
        {
            return this._bookRepository.Table.FirstOrDefault(a => a.Id == ID);
        }

        public int Insert(Book book)
        {
            this._bookRepository.Insert(book);
            return book.Id;
        }

        public void Update(Book book)
        {
            Book existingBook = this._bookRepository.Table.FirstOrDefault(a => a.Id == book.Id);
            book.Title = existingBook.Title;
            book.IsNewRequest = existingBook.IsNewRequest;
            book.CreatedBy = existingBook.CreatedBy;
            book.CreatedOn = existingBook.CreatedOn;
            book.TotalBooks = existingBook.TotalBooks;
            book.ApprovedBooks = existingBook.ApprovedBooks;
            book.NewApprovedBooks = existingBook.NewApprovedBooks;
            book.IssuedBooks = existingBook.IssuedBooks;
            book.TornBooks = existingBook.TornBooks;
            book.AvailableBooks = existingBook.AvailableBooks;

            this._bookRepository.Update(book);
        }
    }
}
