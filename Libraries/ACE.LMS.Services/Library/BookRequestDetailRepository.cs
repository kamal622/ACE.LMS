using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;

namespace ACE.LMS.Services.Library
{
    public class BookRequestDetailRepository
    {
        private readonly IRepository<BookRequestDetail> _bookRequestDetailsRepository;

        public BookRequestDetailRepository(IRepository<BookRequestDetail> bookRequestRepository)
        {
            this._bookRequestDetailsRepository = bookRequestRepository;
        }

        public string GetStudentUserIdForBookRequestDetailId(int Id)
        {
            return this._bookRequestDetailsRepository.Table.Where(w => w.Id == Id).Select(f => f.BookRequest.Student.UserId).FirstOrDefault();
            
        }

        public int GetStudentIdForBookRequestDetailId(int Id)
        {
            return this._bookRequestDetailsRepository.Table.Where(w => w.Id == Id).Select(f => f.BookRequest.Student.Id).FirstOrDefault();

        }
    }
}
