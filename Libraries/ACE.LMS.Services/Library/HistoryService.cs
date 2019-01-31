using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;

namespace ACE.LMS.Services.Library
{
   public class HistoryService
    {
       private readonly IRepository<BookRequestHistory> _bookRequestHistoryRepository;
       private readonly IRepository<LibraryBookHistory> _libraryBookHistoryRepository;

       public HistoryService(IRepository<BookRequestHistory> bookRepository, IRepository<LibraryBookHistory> libraryBookHistory)
       {
           this._bookRequestHistoryRepository = bookRepository;
           this._libraryBookHistoryRepository = libraryBookHistory;
       }

       public void InsertBookRequestHistory(BookRequestHistory history)
       {
           this._bookRequestHistoryRepository.Insert(history);
       }

       public void InsertLibraryBookHistory(LibraryBookHistory libraryBookHistory)
       {
           this._libraryBookHistoryRepository.Insert(libraryBookHistory);
       }

       
    }
}
