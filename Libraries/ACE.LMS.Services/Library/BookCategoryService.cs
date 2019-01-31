using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.LMS.Services.Library
{
    public class BookCategoryService
    {
        private readonly IRepository<BookCategory> _bookCategoryRepository;

        public BookCategoryService(IRepository<BookCategory> bookCategoryRepository)
        {
            this._bookCategoryRepository = bookCategoryRepository;
        }

        public List<BookCategory> GetBookCategories(int bookId)
        {
            return this._bookCategoryRepository.Table.Where(w => w.BookId == bookId).ToList();
        }

        public void InsertUpdateCategories(int bookId, int[] categoryIds)
        {
            BookCategory[] categoriesToDelete = this._bookCategoryRepository.Table.Where(w => w.BookId == bookId && !categoryIds.Contains(w.CategoryId)).ToArray();

            for (int i = 0; i < categoriesToDelete.Length; i++)
                this._bookCategoryRepository.Delete(categoriesToDelete[i]);

            for (int i = 0; i < categoryIds.Length; i++)
            {
                int categoryId = categoryIds[i];
                if (this._bookCategoryRepository.Table.Any(a => a.BookId == bookId && a.CategoryId == categoryId))
                    continue;
                this._bookCategoryRepository.Insert(new BookCategory { BookId = bookId, CategoryId = categoryId });
            }
        }
    }
}
