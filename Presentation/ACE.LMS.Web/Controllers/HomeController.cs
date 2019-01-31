using System.Web;
using ACE.LMS.Services.Library;
using ACE.LMS.Web.Framework.Mvc;
using ACE.LMS.Web.Models;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace ACE.LMS.Web.Controllers
{
    public class HomeController : ACE.LMS.Web.Infrastructure.BaseController
    {
        private readonly BookService _bookService;
        private ApplicationUserManager _userManager;
        private readonly NoticeBoardService _noticeBoardService;

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

        public HomeController(BookService bookService, NoticeBoardService noticeBoardService)
        {
            this._bookService = bookService;
            this._noticeBoardService = noticeBoardService;
        }

        public ActionResult Index()
        {
            var allNotice = this._noticeBoardService.GetNoticeForHome();
            return View(allNotice);
        }

        public JsonNetResult GetBooks()
        {
            //System.Threading.Thread.Sleep(1000 * 5);
            var allData = this._bookService.GetAll();
            var finalData = from a in allData
                            select new
                            {
                                a.Id,
                                a.Title,
                                a.Author,
                                a.Publication
                            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            //base.AddNotification(Framework.UI.NotifyType.Error, "Error notification.", false);
            //base.AddNotification(Framework.UI.NotifyType.Success, "Success notification.", false);

            //try
            //{
            //    int i = 0;
            //    int j = 0;
            //    j = 1 / i;
            //}
            //catch (System.Exception ex)
            //{
            //    base.ErrorNotification(ex);
            //}
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Install()
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));


            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Student"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Librarian"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Librarian";
                roleManager.Create(role);
            }

            if (UserManager.FindByName("Admin@acepolytech.com") == null)
            {
                var user = new ApplicationUser { UserName = "Admin@acepolytech.com", Email = "Admin@acepolytech.com", EmailConfirmed = true };
                user.UserProfile = new ACE.LMS.Web.Models.UserProfile
                {
                    IsActive = true,
                    IsBlocked = false,
                    HasCheckedTerms = true,
                    RegistrationDate = DateTime.Now
                };
                var result = UserManager.Create(user, "Admin@1234");
                UserManager.AddToRole(user.Id, "Admin");
            }

            if (UserManager.FindByName("librarian@acepolytech.com") == null)
            {
                var user = new ApplicationUser { UserName = "librarian@acepolytech.com", Email = "librarian@acepolytech.com", EmailConfirmed = true };
                user.UserProfile = new ACE.LMS.Web.Models.UserProfile
                {
                    IsActive = true,
                    IsBlocked = false,
                    HasCheckedTerms = true,
                    RegistrationDate = DateTime.Now
                };
                var result = UserManager.Create(user, "Test!1234");
                UserManager.AddToRole(user.Id, "Librarian");
            }

            return View();
        }

        public ActionResult Reference()
        {
            return View();

        }
    }
}