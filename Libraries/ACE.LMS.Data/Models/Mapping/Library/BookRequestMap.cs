using System.Data.Entity.ModelConfiguration;
using ACE.LMS.Core.Models.Library;

namespace ACE.LMS.Data.Models.Mapping.Library
{
    public class BookRequestMap : EntityTypeConfiguration<BookRequest>
    {
        public BookRequestMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Status)
                .IsRequired();

            this.Property(t => t.CreatedBy)
                .IsRequired();

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(128);

            this.Property(t => t.AddressWhenStudy)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("BookRequests");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.CollegeId).HasColumnName("CollegeId");
            this.Property(t => t.BranchId).HasColumnName("BranchId");
            this.Property(t => t.SemesterStartDate).HasColumnName("SemesterStartDate");
            this.Property(t => t.SemesterEndDate).HasColumnName("SemesterEndDate");
            this.Property(t => t.IsHostelStay).HasColumnName("IsHostelStay");
            this.Property(t => t.AddressWhenStudy).HasColumnName("AddressWhenStudy");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.RequestDate).HasColumnName("RequestDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasRequired(t => t.CreatedByUser)
                .WithMany(t => t.CreatedByBookRequests)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.UpdatedUser)
                .WithMany(t => t.UpdatedByBookRequests)
                .HasForeignKey(d => d.UpdatedBy);
            this.HasRequired(t => t.Branch)
                .WithMany(t => t.BookRequests)
                .HasForeignKey(d => d.BranchId);
            this.HasRequired(t => t.College)
                .WithMany(t => t.BookRequests)
                .HasForeignKey(d => d.CollegeId);

        }
    }
}
