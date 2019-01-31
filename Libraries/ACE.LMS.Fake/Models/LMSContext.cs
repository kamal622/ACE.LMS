using System.Data.Entity;
using ACE.LMS.Fake.Models.Mapping;

namespace ACE.LMS.Fake.Models
{
    public partial class LMSContext : DbContext
    {
        static LMSContext()
        {
            Database.SetInitializer<LMSContext>(null);
        }

        public LMSContext()
            : base("Name=LMSContext")
        {
        }

        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BooksInventory> BooksInventories { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AspNetRoleMap());
            modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new BooksInventoryMap());
            modelBuilder.Configurations.Add(new StudentMap());
        }
    }
}
