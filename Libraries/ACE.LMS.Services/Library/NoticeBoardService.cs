using ACE.LMS.Core.Data;
using ACE.LMS.Core.Models.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.LMS.Services.Library
{
    public class NoticeBoardService
    {
        private readonly IRepository<NoticeBoard> _noticeBoardRepo;

        public NoticeBoardService(IRepository<NoticeBoard> noticeBoardRepo)
        {
            this._noticeBoardRepo = noticeBoardRepo;
        }

        public List<NoticeBoard> GetAllNotice()
        {
            return this._noticeBoardRepo.Table.ToList();
        }

        public void SaveNotice(NoticeBoard noticeBoard)
        {
            DateTime endDate = noticeBoard.EndDate.Value;
            noticeBoard.StartDate = noticeBoard.StartDate.Date;
            noticeBoard.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            if (noticeBoard.Id <= 0)
                this._noticeBoardRepo.Insert(noticeBoard);
            else
            {
                var existingNotice = this._noticeBoardRepo.Table.Where(w => w.Id == noticeBoard.Id).FirstOrDefault();
                noticeBoard.CreatedDate = existingNotice.CreatedDate;
                noticeBoard.CreatedBy = existingNotice.CreatedBy;
                this._noticeBoardRepo.Update(noticeBoard);
            }
        }

        public void ActiveInactiveNotice(int noticeId, bool IsActive)
        {
            NoticeBoard existing = this._noticeBoardRepo.GetById(noticeId);
            if (existing != null)
            {
                existing.IsActive = IsActive;
                this._noticeBoardRepo.Update(existing);
            }
        }

        public List<NoticeBoard> GetNoticeForHome()
        {
            return this._noticeBoardRepo.Table.Where(w => w.StartDate <= DateTime.Now && w.EndDate >= DateTime.Now && w.IsActive == true).ToList();
        }
    }
}
