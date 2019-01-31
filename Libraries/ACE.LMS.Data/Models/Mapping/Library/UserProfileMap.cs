using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core.Models.Library;

namespace ACE.LMS.Data.Models.Mapping.Library
{
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
}
