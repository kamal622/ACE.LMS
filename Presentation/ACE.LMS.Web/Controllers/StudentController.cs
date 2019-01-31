using ACE.LMS.Services.Library;
using ACE.LMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ACE.LMS.Web.Models;
using ACE.LMS.Web.App_Constant;

namespace ACE.LMS.Web.Controllers
{
    [Authorize(Roles="Student")]
    public class StudentController : ACE.LMS.Web.Infrastructure.BaseController
    {
        private readonly StudentService _studentService;
        private readonly BookRequestService _bookRequestService;
        public StudentController(BookRequestService bookRequestService, StudentService studentService)
        {
            this._studentService = studentService;
            this._bookRequestService = bookRequestService;
        }

        //
        // GET: /Student/
        //[Authorize(Roles = "Student")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonNetResult GetRequests()
        {
            var allData = from a in this._bookRequestService.GetRequestDetailsForStudent(User.Identity.GetUserId())
                          orderby a.BookRequest.RequestDate descending
                          select new StudentViewModels.BookRequestDetailsData
                          {
                              Id = a.Id,
                              RequestId = a.BookRequest.Id,
                              BookTitle = a.Book.Title,
                              BookAuthor = a.Book.Author,
                              BookPublisher = a.Book.Publication,
                              Subject = a.Subject,
                              RequestDate = a.BookRequest.RequestDate,
                              IssueDate = a.BookIssues.Select(s=>s.IssueDate).FirstOrDefault(),
                              ReturnDate = a.BookIssues.Select(s=>s.BookReturns.Select(s2=>s2.ReturnDate).FirstOrDefault()).FirstOrDefault(),
                              ReturnBefore = a.BookIssues.Select(s => s.ReturnOnOrBefore).FirstOrDefault(),
                              Status = a.Status == 1 ? "Pending" : a.Status == 2 ? "Approved" : a.Status == 3 ? "Rejected" : a.Status == 4 ? "Issued" : "Returned"
                          };

            return JsonNet(allData, JsonRequestBehavior.AllowGet);
        }
    }
}