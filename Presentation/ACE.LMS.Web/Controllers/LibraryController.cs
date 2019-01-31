using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using ACE.LMS.Core.Models.Library;
using ACE.LMS.Services.Library;
using ACE.LMS.Web.App_Constant;
using ACE.LMS.Web.Framework.Mvc;
using ACE.LMS.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Mvc.Html;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using ACE.LMS.Services.Directory;
using ACE.LMS.Core;
using System.Text;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ACE.LMS.Web.Controllers
{
    [Authorize]
    public class LibraryController : ACE.LMS.Web.Infrastructure.BaseController
    {
        private readonly StudentService _studentService;
        private readonly CollegeBranchService _collegeBranchService;
        private readonly BookService _bookService;
        private readonly BookRequestService _bookRequestService;
        private readonly UserProfileService _userProfileService;
        private readonly DirectoryService _directoryService;
        private readonly HistoryService _historyService;
        private ApplicationUserManager _userManager;
        private readonly WebHelper _webHelper;
        private readonly LibraryBookService _libraryBookService;
        private readonly CategoryService _categoryService;
        private readonly BookCategoryService _bookCategoryService;
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
        public int Width = 110;
        public int Height = 120;

        public LibraryController(StudentService studentService,
            CollegeBranchService collegeBranchService,
            BookService bookService,
            BookRequestService bookRequestService,
            UserProfileService userProfileService,
            DirectoryService directoryService,
            HistoryService historyService,
            WebHelper webHelper,
            LibraryBookService libraryBookService,
            CategoryService categoryService,
            BookCategoryService bookCategoryService)
        {
            this._studentService = studentService;
            this._collegeBranchService = collegeBranchService;
            this._bookService = bookService;
            this._bookRequestService = bookRequestService;
            this._userProfileService = userProfileService;
            this._directoryService = directoryService;
            this._historyService = historyService;
            this._webHelper = webHelper;
            this._libraryBookService = libraryBookService;
            this._categoryService = categoryService;
            this._bookCategoryService = bookCategoryService;
        }

        [Authorize(Roles = "Librarian")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Student")]
        public ActionResult BookRequest()
        {
            if (User.IsInRole("Student"))
            {
                var userId = User.Identity.GetUserId();
                var student = this._studentService.GetStudentByUserId(userId);
                if (student == null)
                {
                    student = new Student();
                    student.DOB = DateTime.Now;
                    student.Email = User.Identity.Name;
                    //student.ImagePath = "default-profile-icon.png";
                    student.Id = 0;
                    student.UserId = userId;
                }
                return View(new BookRequestViewModel { Student = student });
            }
            else
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonNetResult BookRequest(BookRequestViewModel model, string button)
        {
            //if (button.Equals("AddNew", StringComparison.OrdinalIgnoreCase))
            //{
            //    if (model.BookRequestDetails == null)
            //        model.BookRequestDetails = new List<BookRequestDetail>();
            //    int Id = model.BookRequestDetails.Max(m => m.Id) + 1;
            //    model.BookRequestDetails.Add(new BookRequestDetail { Id = Id });
            //}
            //else if (button.StartsWith("DeleteRow", StringComparison.OrdinalIgnoreCase))
            //{
            //    int Id = Convert.ToInt32(button.Split('_')[1]);
            //    model.BookRequestDetails = model.BookRequestDetails.Where(w => w.Id != Id).ToList();
            //}           

            try
            {
                string userId = User.Identity.GetUserId();
                //model.Student = this._studentService.GetStudentByUserId(userId);
                if (!ModelState.IsValid)
                {
                    JsonResponse response = new JsonResponse { Messages = new List<string>(), MsgType = MessageType.Validations };
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                        foreach (ModelError error in modelState.Errors)
                            response.Messages.Add(error.ErrorMessage);
                    return JsonNet(response, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var bookRequestDetails = model.BookRequestDetails.Where(w => !string.IsNullOrEmpty(w.Subject) && !string.IsNullOrEmpty(w.Title)
                    && !string.IsNullOrEmpty(w.Publication) && !string.IsNullOrEmpty(w.Author)).ToList();

                    var partialBookRequests = model.BookRequestDetails.Where(w => !bookRequestDetails.Contains(w) && (!string.IsNullOrEmpty(w.Subject) || !string.IsNullOrEmpty(w.Title)
                    || !string.IsNullOrEmpty(w.Publication) || !string.IsNullOrEmpty(w.Author))).ToList();

                    var messages = new List<string>();

                    if (string.Equals(model.College.Name.Trim(), "Other", StringComparison.OrdinalIgnoreCase))
                        messages.Add("College name is not allowed.");
                    if (string.Equals(model.Branch.Name.Trim(), "Other", StringComparison.OrdinalIgnoreCase))
                        messages.Add("Branch name is not allowed");

                    if (partialBookRequests.Any())
                    {
                        for (int i = 0; i < partialBookRequests.Count; i++)
                        {
                            if (string.IsNullOrEmpty(partialBookRequests[i].Subject))
                                messages.Add(string.Format("Subject required for Sr.no. {0}.", partialBookRequests[i].Id));
                            if (string.IsNullOrEmpty(partialBookRequests[i].Title))
                                messages.Add(string.Format("Title required in Sr.no. {0}.", partialBookRequests[i].Id));
                            if (string.IsNullOrEmpty(partialBookRequests[i].Publication))
                                messages.Add(string.Format("Publication required in Sr.no. {0}.", partialBookRequests[i].Id));
                            if (string.IsNullOrEmpty(partialBookRequests[i].Author))
                                messages.Add(string.Format("Author required in Sr.no. {0}.", partialBookRequests[i].Id));
                        }
                    }
                    else if (!bookRequestDetails.Any())
                    {
                        messages.Add("Atleast one text book request is required.");
                    }

                    if (messages.Any())
                        return JsonNet(new JsonResponse { MsgType = MessageType.Validations, Messages = messages }, JsonRequestBehavior.AllowGet);
                    using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
                    {
                        if (model.College.Id <= 0)
                        {
                            model.College.CreatedOn = DateTime.Now;
                            model.College.Id = this._collegeBranchService.InsertCollege(model.College);
                        }

                        if (model.Branch.Id <= 0)
                        {
                            model.Branch.CreatedOn = DateTime.Now;
                            model.Branch.Id = this._collegeBranchService.InsertBranch(model.Branch);
                        }

                        //model.Student.UserId = userId;
                        if (model.Student.Id > 0)
                        {
                            model.Student.UpdatedBy = userId;
                            model.Student.UpdatedOn = DateTime.Now;
                            //model.Student.EnrollmentNo = String.Format("{0}-{1}-{2}-A", DateTime.Now.ToString("yy"), model.Branch.Name.Substring(0, 2), model.Student.Id.ToString("0000"));
                            //this._studentService.Update(model.Student); // Student can not update. Need to request admin to update info
                        }
                        else
                        {
                            model.Student.CreatedBy = userId;
                            model.Student.CreatedOn = DateTime.Now;
                            model.Student.EnrollmentNo = "E";
                            model.Student.Id = this._studentService.Insert(model.Student);
                            string[] branchNameArray = model.Branch.Name.Split(new char[] { ' ' });
                            if (branchNameArray.Length >= 2)
                                model.Student.EnrollmentNo = String.Format("{0}-{1}{2}-{3}-A", DateTime.Now.ToString("yy"), branchNameArray[0][0].ToString().ToUpper(), branchNameArray[1][0].ToString().ToUpper(), model.Student.Id.ToString("0000"));
                            else
                                model.Student.EnrollmentNo = String.Format("{0}-{1}-{2}-A", DateTime.Now.ToString("yy"), model.Branch.Name.Substring(0, 2).ToUpper(), model.Student.Id.ToString("0000"));
                            this._studentService.Update(model.Student); // Student can not update. Need to request admin if require any change
                        }

                        model.BookRequest.StudentId = model.Student.Id;
                        model.BookRequest.CollegeId = model.College.Id;
                        model.BookRequest.BranchId = model.Branch.Id;
                        model.BookRequest.Subject = "NA";
                        model.BookRequest.RequestDate = DateTime.Now;
                        model.BookRequest.CreatedBy = userId;
                        model.BookRequest.CreatedOn = DateTime.Now;
                        model.BookRequest.Id = this._bookRequestService.InsertBookRequest(model.BookRequest);

                        foreach (var item in bookRequestDetails)
                        {
                            int bookId = 0;
                            int.TryParse(item.BookId, out bookId);
                            item.BookId = Convert.ToString(bookId);

                            Book existingBook = null;
                            if (bookId > 0)
                                existingBook = this._bookService.GetBookById(bookId);

                            if (existingBook != null)
                            {
                                item.Title = existingBook.Title;
                                if ((!existingBook.Author.Equals(item.Author, StringComparison.OrdinalIgnoreCase) || !existingBook.Publication.Equals(item.Publication, StringComparison.OrdinalIgnoreCase)))
                                {
                                    bookId = 0;
                                    item.BookId = "0";
                                }

                            }

                            if (bookId <= 0)
                            {
                                var book = new Book
                                {
                                    Title = item.Title,
                                    Author = item.Author,
                                    Subject = item.Subject,
                                    Publication = item.Publication,
                                    IsNewRequest = true,
                                    TotalBooks = 0,
                                    ApprovedBooks = 0,
                                    NewApprovedBooks = 0,
                                    IssuedBooks = 0,
                                    TornBooks = 0,
                                    AvailableBooks = 0
                                };
                                bookId = this._bookService.Insert(book);
                                item.BookId = Convert.ToString(bookId);

                            }

                            var bookRequestDetail = new BookRequestDetail
                            {
                                RequestId = model.BookRequest.Id,
                                BookId = bookId,
                                Status = (int)BookRequestStatus.Pending,
                                Subject = item.Subject
                            };

                            item.Id = this._bookRequestService.InsertBookRequestDetail(bookRequestDetail);
                            this._historyService.InsertBookRequestHistory(new BookRequestHistory { BookRequestDetailId = item.Id, Status = (int)BookRequestStatus.Pending, CreatedBy = userId, CreatedOn = DateTime.Now });
                        }
                        scope.Complete();
                    }

                    var emailText = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/EmailTemplates/RequestReceived.html"));
                    emailText = emailText.Replace("{{BASE_URL}}", _webHelper.GetStoreHost(false));
                    System.Web.UI.WebControls.Table tbl = new System.Web.UI.WebControls.Table();
                    tbl.BorderColor = Color.Black;
                    tbl.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                    tbl.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1);
                    tbl.Width = System.Web.UI.WebControls.Unit.Percentage(100);
                    System.Web.UI.WebControls.TableRow tr = null;
                    System.Web.UI.WebControls.TableCell col1 = null;
                    System.Web.UI.WebControls.TableCell col2 = null;
                    System.Web.UI.WebControls.TableCell col3 = null;
                    System.Web.UI.WebControls.TableCell col4 = null;
                    System.Web.UI.WebControls.TableCell col5 = null;

                    tr = new System.Web.UI.WebControls.TableRow();
                    col1 = new System.Web.UI.WebControls.TableCell();
                    col2 = new System.Web.UI.WebControls.TableCell();
                    col3 = new System.Web.UI.WebControls.TableCell();
                    col4 = new System.Web.UI.WebControls.TableCell();
                    col5 = new System.Web.UI.WebControls.TableCell();

                    col1.Text = "Sr#";
                    col2.Text = "Subject";
                    col3.Text = "Title";
                    col4.Text = "Author";
                    col5.Text = "Publication";
                    col1.Style.Add("font-weight", "bold");
                    col2.Style.Add("font-weight", "bold");
                    col3.Style.Add("font-weight", "bold");
                    col4.Style.Add("font-weight", "bold");
                    col5.Style.Add("font-weight", "bold");

                    tr.Cells.AddRange(new System.Web.UI.WebControls.TableCell[] { col1, col2, col3, col4, col5 });
                    tbl.Rows.Add(tr);
                    int j = 0;
                    foreach (var item in bookRequestDetails)
                    {
                        j += 1;
                        tr = new System.Web.UI.WebControls.TableRow();
                        col1 = new System.Web.UI.WebControls.TableCell();
                        col2 = new System.Web.UI.WebControls.TableCell();
                        col3 = new System.Web.UI.WebControls.TableCell();
                        col4 = new System.Web.UI.WebControls.TableCell();
                        col5 = new System.Web.UI.WebControls.TableCell();

                        col1.Text = Convert.ToString(j);
                        col2.Text = item.Subject;
                        col3.Text = item.Title;
                        col4.Text = item.Author;
                        col5.Text = item.Publication;
                        tr.Cells.AddRange(new System.Web.UI.WebControls.TableCell[] { col1, col2, col3, col4, col5 });
                        tbl.Rows.Add(tr);
                    }

                    StringBuilder sb = new StringBuilder();
                    System.Web.UI.HtmlTextWriter ht = new System.Web.UI.HtmlTextWriter(new System.IO.StringWriter(sb));
                    tbl.RenderControl(ht);
                    string html = ht.InnerWriter.ToString();

                    emailText = emailText.Replace("{{BOOK_REQUESTS}}", html);
                    UserManager.SendEmail(model.Student.UserId, "Book Request Received", emailText);

                    return JsonNet(new JsonResponse { MsgType = MessageType.Success, Messages = new List<string>(), RedirectUrl = Url.Action("RequestSuccess", "Library", null, Request.Url.Scheme) }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
                var messages = new List<string>();
                messages.Add(ex.Message + ex.InnerException ?? ex.InnerException.Message);
                return JsonNet(new JsonResponse { MsgType = MessageType.Error, Messages = messages }, JsonRequestBehavior.AllowGet);
            }
            //return PartialView("_BookRequestDetails", model.BookRequestDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonNetResult StudentFeedBack(StudentFeedBackViewModel model)
        {
            var student = this._studentService.GetStudentByUserId(User.Identity.GetUserId());
            student.IsLike = model.isLike;
            student.IsLikeBookShare = model.IsLikeBookShare;
            student.IsLikeContribution = model.IsLikeContribution;
            student.IsLikeVoluntary = model.IsLikeVoluntary;
            this._studentService.Update(student);
            return JsonNet(new JsonResponse { MsgType = MessageType.Success, Messages = new List<string>(), RedirectUrl = Url.Action("Index", "Student", null, Request.Url.Scheme) }, JsonRequestBehavior.AllowGet);
        }

        public JsonNetResult SaveStudent(Student student, List<Book> books, College college, BookRequest bookRequest)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return JsonNet(new ReturnData { Message = "Please fill all required information." }, JsonRequestBehavior.AllowGet);
                //}

                //VALIDATION FOR BOOK REQUEST
                if (books == null || !books.Any())
                    return JsonNet(new ReturnData { Message = "Please add atleast one book." }, JsonRequestBehavior.AllowGet);

                for (int i = 0; i < books.Count(); i++)
                {
                    Book book = books[i];
                    if (!string.IsNullOrEmpty(book.Title) || !string.IsNullOrEmpty(book.Author) || !string.IsNullOrEmpty(book.Publication) || !string.IsNullOrEmpty(book.Subject))
                    {
                        if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author) || string.IsNullOrEmpty(book.Publication) || string.IsNullOrEmpty(book.Subject))
                            return JsonNet(new ReturnData { Message = string.Format("Information for SrNo: {0} not filled properly.", i + 1) }, JsonRequestBehavior.AllowGet);
                    }
                }


                //VALIDATION FOR STUDENT PERSONAL INFO
                if (student.FirstName == null || student.LastName == null || bookRequest.CollegeId == null || bookRequest.BranchId == null || student.ImagePath == "")
                {
                    return JsonNet(new ReturnData { Message = "Please fill all personal information." }, JsonRequestBehavior.AllowGet);

                }


                //System.Threading.Thread.Sleep(1000 * 5);
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
                {

                    // var user = new ApplicationUser { UserName = student.Email, Email = student.Email };
                    //  var result = UserManager.Create(user, "Abc123#");


                    //UserManager.AddToRole(user.Id, "Student");

                    student.UserId = User.Identity.GetUserId();

                    //Insert college if new
                    if (college.Name != null)
                    {
                        college.CreatedOn = DateTime.Now;
                        bookRequest.CollegeId = this._collegeBranchService.InsertCollege(college);
                    }

                    //Insert Student

                    int studentId = student.Id;
                    if (studentId <= 0)
                    {
                        student.CreatedBy = User.Identity.GetUserId();
                        student.CreatedOn = DateTime.Now;
                        studentId = this._studentService.Insert(student);
                    }
                    else
                    {
                        student.UpdatedBy = User.Identity.GetUserId();
                        student.UpdatedOn = DateTime.Now;
                        this._studentService.Update(student);
                    }
                    //Insert Request

                    bookRequest.StudentId = studentId;
                    // bookRequest.CollegeId = student.CollegeId;
                    //  bookRequest.BranchId = student.BranchId;
                    bookRequest.RequestDate = DateTime.Now;
                    bookRequest.CreatedBy = User.Identity.GetUserId();
                    bookRequest.CreatedOn = DateTime.Now;
                    int bookRequestId = this._bookRequestService.InsertBookRequest(bookRequest);


                    //Insert Request Detail (Check New book or old book
                    NewBookRequestDetail newBookRequestDetail;

                    foreach (var book in books)
                    {
                        if (string.IsNullOrEmpty(book.Title))
                            continue;

                        BookRequestDetail bookRequestDetail;
                        if (book.Title.IsInt())
                        {
                            bookRequestDetail = new BookRequestDetail();
                            bookRequestDetail.BookId = int.Parse(book.Title);
                            bookRequestDetail.RequestId = bookRequestId;
                            bookRequestDetail.Status = (int)BookRequestStatus.Pending;
                            bookRequestDetail.Subject = book.Subject;
                            this._bookRequestService.InsertBookRequestDetail(bookRequestDetail);
                        }
                        else
                        {
                            // book.IsNew = true;
                            int bookId = this._bookService.Insert(book);

                            bookRequestDetail = new BookRequestDetail();
                            bookRequestDetail.BookId = bookId;
                            bookRequestDetail.RequestId = bookRequestId;
                            bookRequestDetail.Status = (int)BookRequestStatus.Pending;
                            bookRequestDetail.Subject = book.Subject;
                            this._bookRequestService.InsertBookRequestDetail(bookRequestDetail);
                        }
                    }

                    scope.Complete();
                    // /Images/tempFile.jpg

                    try
                    {
                        if (!String.IsNullOrEmpty(student.ImagePath) && studentId != 0)
                        {
                            int index = student.ImagePath.LastIndexOf('.');
                            string ext = student.ImagePath.Substring(index + 1);

                            string subPath = Server.MapPath("../Uploads/ProfilePics"); // your code goes here
                            //  System.IO.File.Move(subPath+"//"+student.ImagePath,subPath+"//"+"st"+studentID+ext);
                            FileInfo file = new FileInfo(subPath + "\\" + student.ImagePath);
                            string newName = "\\st" + studentId + "." + ext;
                            file.CopyTo(subPath + newName, true);
                            file.Delete();
                            student.ImagePath = newName;
                            this._studentService.Update(student);

                        }
                    }
                    catch (Exception ex)
                    {


                    }

                    //studentID
                    return JsonNet(new ReturnData { Message = "Success" }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return JsonNet(new ReturnData { Message = "Oops! registration failed. Please contact administrator." }, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize(Roles = "Student")]
        public ActionResult RequestSuccess()
        {
            return View();
        }

        public JsonNetResult GetAllBooks()
        {
            List<Book> books = this._bookService.GetAll();
            var finalData = from a in books
                            orderby a.Title
                            select new
                            {
                                a.Id,
                                a.Title
                            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);
        }

        public JsonNetResult GetBookDetailById(int bookID)
        {
            Book book = this._bookService.GetBookById(bookID);
            var finalData = new
            {
                Author = book.Author,
                Publication = book.Publication
            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);
        }

        public JsonNetResult GetAllColleges()
        {
            List<College> colleges = this._collegeBranchService.GetAllColleges();
            colleges.Add(new College { Id = -1, Name = "Other" });
            var finalData = from a in colleges
                            select new
                            {
                                a.Id,
                                a.Name
                            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);
        }

        public JsonNetResult GetDetailByCollegeId(int collegeID)
        {
            College college = this._collegeBranchService.GetDetailByCollegeId(collegeID);
            var finalData = new
            {
                Id = college.Id,
                Name = college.Name,
                Address = college.Address,
                HasBookBank = college.HasBookBank
            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);

        }

        //GetAllBranches
        public JsonNetResult GetAllBranches()
        {
            List<Branch> branches = this._collegeBranchService.GetAllBranches();
            branches.Add(new Branch { Id = -1, Name = "Other" });
            var finalData = from a in branches
                            select new
                            {
                                a.Id,
                                a.Name
                            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);
        }

        public JsonNetResult GetStudentDetailById(int studentId)
        {
            Student student = this._studentService.GetStudentDetailById(studentId);
            var finalData = new
                            {
                                Id = student.Id,
                                FullName = student.FirstName + " " + student.LastName,
                                student.FirstName,
                                student.LastName,
                                student.FatherName,
                                student.FatherNative,
                                student.MotherName,
                                student.MotherNative,
                                student.PresentAddress,
                                student.PermanentAddress,
                                student.DOB,
                                student.Mobile,
                                student.Phone,
                                student.Email,
                                student.SSCResult,
                                student.SSCPassingYear,
                                student.HSCPassingYear,
                                student.HSCResult,
                                student.OtherReference,
                                student.ReferenceMobile,
                                student.FatherOccupation,
                                // student.CollegeId,
                                // student.College.Address,
                                //  student.College.HasBookBank,
                                // student.BranchId,
                                student.ImagePath,
                                student.InHostel,
                                student.CreatedOn,
                                student.CreatedBy
                            };

            return JsonNet(finalData, JsonRequestBehavior.AllowGet);
        }

        public JsonNetResult GetStudentData(string searchString)
        {
            var allData = this._studentService.GetStudentNames(searchString);
            var finalData = from a in allData
                            select new
                            {
                                Id = a.Id,
                                Name = a.FirstName + " " + a.LastName
                            };
            return JsonNet(finalData.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonNetResult UploadProfilePic()
        {
            //Reference: http://stackoverflow.com/questions/14575787/how-to-upload-image-in-asp-net-mvc-4-using-ajax-or-any-other-technique-without-p
            try
            {
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {

                    var pic = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                    int index = pic.FileName.LastIndexOf('.');
                    string ext = pic.FileName.Substring(index).ToLower();
                    if (ext != ".jpg" && ext != ".png" && ext != ".gif" && ext != ".jpeg")
                    {
                        return JsonNet(new { PhotoMessage = "Allowed file types are .jpg, .png , .gif .", FileName = "" }, JsonRequestBehavior.AllowGet);
                    }

                    //Create directory
                    string subPath = Server.MapPath("../Uploads/ProfilePics"); // your code goes here

                    bool exists = System.IO.Directory.Exists(subPath);
                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));


                    string filename = Guid.NewGuid().ToString() + ext;

                    //string imageSavePath = "";
                    pic.SaveAs(subPath + "\\" + filename);


                    Image imgOriginal = Image.FromFile(subPath + "\\" + filename);

                    //pass in whatever value you want
                    Image imgActual = Scale(imgOriginal);
                    imgOriginal.Dispose();
                    imgActual.Save(subPath + "\\" + filename);
                    imgActual.Dispose();



                    System.IO.FileInfo fi = new System.IO.FileInfo(pic.FileName);
                    return JsonNet(new { PhotoMessage = "Success", FileName = filename }, JsonRequestBehavior.AllowGet);

                }
                return JsonNet(new { PhotoMessage = "Unable to upload image.", FileName = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return JsonNet(new { PhotoMessage = "Error uploading image. Please contact administrator.", FileName = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        private Image Scale(Image imgPhoto)
        {
            float sourceWidth = imgPhoto.Width;
            float sourceHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // force resize, might distort image
            if (Width != 0 && Height != 0)
            {
                destWidth = Width;
                destHeight = Height;
            }
            // change size proportially depending on width or height
            else if (Height != 0)
            {
                destWidth = (float)(Height * sourceWidth) / sourceHeight;
                destHeight = Height;
            }
            else
            {
                destWidth = Width;
                destHeight = (float)(sourceHeight * Width / sourceWidth);
            }

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
                                        PixelFormat.Format32bppPArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
                new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }

        [Authorize(Roles = "Student")]
        public ActionResult TermsAndConditions()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
        public ActionResult TermsAndConditions(TermsAndConditionViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            //Session["HasCheckedTerms"] = true;
            var currentUserId = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(currentUserId);

            this._userProfileService.UpdateTermsNConditionFlag(currentUser.UserProfile.Id);
            return RedirectToAction("Index", "Student");
        }

        public JsonNetResult GetCities()
        {
            var allData = from a in this._directoryService.GetAllCities()
                          orderby a.Name
                          select new
                          {
                              Id = a.Id,
                              Name = a.Name
                          };

            return JsonNet(allData, JsonRequestBehavior.AllowGet);
        }

        #region BookMaintenance

        [Authorize(Roles = "Librarian,Admin")]
        public ActionResult BookMaintenance()
        {
            return View();
        }

        [HttpPost]
        public JsonNetResult SaveBook(BookMaintenance bookMaintenance)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    JsonResponse response = new JsonResponse { Messages = new List<string>(), MsgType = MessageType.Validations };
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                        foreach (ModelError error in modelState.Errors)
                            response.Messages.Add(error.ErrorMessage);
                    return JsonNet(response, JsonRequestBehavior.AllowGet);
                }

                bookMaintenance.Book.Subject = "NA";

                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
                {
                    if (bookMaintenance.Book.Id > 0)
                        this._bookService.Update(bookMaintenance.Book);
                    else
                        bookMaintenance.Book.Id = this._bookService.Insert(bookMaintenance.Book);

                    bookMaintenance.LibraryBook.BookId = bookMaintenance.Book.Id;
                    if (bookMaintenance.LibraryBook.Id > 0)
                        this._libraryBookService.Update(bookMaintenance.LibraryBook);
                    else
                        bookMaintenance.LibraryBook.Id = this._libraryBookService.Insert(bookMaintenance.LibraryBook);

                    string categoryIds = bookMaintenance.CategoryIds;
                    int[] categories = categoryIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Int32.Parse(s)).ToArray();
                    this._bookCategoryService.InsertUpdateCategories(bookMaintenance.Book.Id, categories);

                    scope.Complete();
                }
                return JsonNet(new JsonResponse { MsgType = MessageType.Success, Messages = new List<string>() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
                var messages = new List<string>();
                messages.Add(ex.Message + ex.InnerException ?? ex.InnerException.Message);
                return JsonNet(new JsonResponse { MsgType = MessageType.Error, Messages = messages }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonNetResult SaveOnlyBook(int Id, string title, string author, string publication, string categoryIds)
        {
            try
            {
                JsonResponse response = new JsonResponse { Messages = new List<string>(), MsgType = MessageType.Validations };
                if (string.IsNullOrEmpty(title))
                    response.Messages.Add("Book title is required.");
                if (string.IsNullOrEmpty(author))
                    response.Messages.Add("Author is required.");
                if (string.IsNullOrEmpty(publication))
                    response.Messages.Add("Publication is required.");
                if (response.Messages.Count() > 0)
                    return JsonNet(response, JsonRequestBehavior.AllowGet);
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))  // Testing Required...
                {
                    if (Id > 0)
                        this._bookService.Update(new Book { Id = Id, Title = title, Author = author, Publication = publication, Subject = "NA", IsNewRequest = false, CreatedBy = User.Identity.Name, CreatedOn = DateTime.Now, TotalBooks = 0, ApprovedBooks = 0, NewApprovedBooks = 0, IssuedBooks = 0, TornBooks = 0, AvailableBooks = 0 });
                    else
                        Id = this._bookService.Insert(new Book { Id = 0, Title = title, Author = author, Publication = publication, Subject = "NA", IsNewRequest = false, CreatedBy = User.Identity.Name, CreatedOn = DateTime.Now, TotalBooks = 0, ApprovedBooks = 0, NewApprovedBooks = 0, IssuedBooks = 0, TornBooks = 0, AvailableBooks = 0 });

                    int[] categories = categoryIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Int32.Parse(s)).ToArray();
                    this._bookCategoryService.InsertUpdateCategories(Id, categories);
                    scope.Complete();
                }
                return JsonNet(new JsonResponse { MsgType = MessageType.Success, Messages = new List<string>() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
                var messages = new List<string>();
                messages.Add(ex.Message + ex.InnerException ?? ex.InnerException.Message);
                return JsonNet(new JsonResponse { MsgType = MessageType.Error, Messages = messages }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonNetResult GetAllCategories()
        {
            var allCategories = this._categoryService.GetAllCategories();
            var allData = allCategories.Select(category => new TreeViewData { Name = category.Name, Id = category.Id, ParentId = category.ParentCategoryId ?? -1, Value = category.Id }).ToList();
            return JsonNet(allData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult GetBooks(int categoryId, string searchKeyword)
        {
            List<Book> books = this._bookService.GetBooks(categoryId, searchKeyword.Split(new char[] { ' ', ',', ';', '|', '+' }, StringSplitOptions.RemoveEmptyEntries));
            var finalData = from a in books
                            select new
                            {
                                Id = a.Id,
                                Title = a.Title,
                                Author = a.Author,
                                Subject = a.Subject,
                                Publication = a.Publication,
                                IsNewRequest = a.IsNewRequest,
                                ApprovedBooks = a.ApprovedBooks,
                                TotalBooks = a.TotalBooks,
                                NewApprovedBooks = a.NewApprovedBooks,
                                IssuedBooks = a.IssuedBooks,
                                TornBooks = a.TornBooks,
                                AvailableBooks = a.AvailableBooks

                            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult GetBookDetails(int bookId)
        {
            var allData = this._libraryBookService.GetBookDetails(bookId);

            var finalData = from a in allData
                            select new
                            {
                                a.Id,
                                a.BookNo,
                                a.ClassNo,
                                a.Price,
                                a.PurchaseDate,
                                Status = a.Status == 1 ? "Available" : a.Status == 2 ? "Issued" : "Torn",
                                a.HasCD,
                                Pages = a.Pages == null ? 0 : a.Pages,
                                PublishedYear = a.PublishedYear == null ? 0 : a.PublishedYear,
                                a.ISBN,
                                a.Store
                            };
            return JsonNet(finalData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonNetResult GetBookCategories(int bookId)
        {
            var bookCategories = this._bookCategoryService.GetBookCategories(bookId);
            var allData = bookCategories.Select(s => new { s.CategoryId }).ToList();
            return JsonNet(allData, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }

}