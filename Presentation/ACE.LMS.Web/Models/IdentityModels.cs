using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    [TableName("UserProfile")]
    public partial class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public bool HasCheckedTerms { get; set; }
        public System.DateTime RegistrationDate { get; set; }
        public Nullable<System.DateTime> EmailConfirmationDate { get; set; }
        public Nullable<System.DateTime> TermsAgreementDate { get; set; }
        public int BlockCount { get; set; }
        public string LastBlockedReason { get; set; }
        public string Note { get; set; }
    }

    public class UserProfileMap : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            
            this.Property(t => t.LastBlockedReason)
                .HasMaxLength(500);

            this.Property(t => t.Note)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("UserProfile");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsBlocked).HasColumnName("IsBlocked");
            this.Property(t => t.HasCheckedTerms).HasColumnName("HasCheckedTerms");
            this.Property(t => t.RegistrationDate).HasColumnName("RegistrationDate");
            this.Property(t => t.EmailConfirmationDate).HasColumnName("EmailConfirmationDate");
            this.Property(t => t.TermsAgreementDate).HasColumnName("TermsAgreementDate");
            this.Property(t => t.BlockCount).HasColumnName("BlockCount");
            this.Property(t => t.LastBlockedReason).HasColumnName("LastBlockedReason");
            this.Property(t => t.Note).HasColumnName("Note");
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ACELMSContext", throwIfV1Schema: false)
        {
        }

        public System.Data.Entity.DbSet<UserProfile> UserProfile { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new UserProfileMap());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}