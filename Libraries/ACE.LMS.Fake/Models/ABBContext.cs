using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ACE.LMS.Fake.Models.Mapping;

namespace ACE.LMS.Fake.Models
{
    public partial class ABBContext : DbContext
    {
        static ABBContext()
        {
            Database.SetInitializer<ABBContext>(null);
        }

        public ABBContext()
            : base("Name=ABBContext")
        {
        }

        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookIssue> BookIssues { get; set; }
        public DbSet<BookRequestDetail> BookRequestDetails { get; set; }
        public DbSet<BookRequestHistory> BookRequestHistories { get; set; }
        public DbSet<BookRequest> BookRequests { get; set; }
        public DbSet<BookReturn> BookReturns { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<LibraryBookHistory> LibraryBookHistories { get; set; }
        public DbSet<LibraryBook> LibraryBooks { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<NewBookRequestDetail> NewBookRequestDetails { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AspNetRoleMap());
            modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new BookCategoryMap());
            modelBuilder.Configurations.Add(new BookIssueMap());
            modelBuilder.Configurations.Add(new BookRequestDetailMap());
            modelBuilder.Configurations.Add(new BookRequestHistoryMap());
            modelBuilder.Configurations.Add(new BookRequestMap());
            modelBuilder.Configurations.Add(new BookReturnMap());
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new BranchMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new CollegeMap());
            modelBuilder.Configurations.Add(new LibraryBookHistoryMap());
            modelBuilder.Configurations.Add(new LibraryBookMap());
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new NewBookRequestDetailMap());
            modelBuilder.Configurations.Add(new NoticeBoardMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
        }
    }
}
