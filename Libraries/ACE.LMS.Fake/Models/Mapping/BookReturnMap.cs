using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Fake.Models.Mapping
{
    public class BookReturnMap : EntityTypeConfiguration<BookReturn>
    {
        public BookReturnMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Notes)
                .HasMaxLength(250);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(128);

            this.Property(t => t.SubmittedBy)
                .HasMaxLength(100);

            this.Property(t => t.Mobile)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("BookReturn");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ReturnDate).HasColumnName("ReturnDate");
            this.Property(t => t.BookIssueId).HasColumnName("BookIssueId");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.LateDays).HasColumnName("LateDays");
            this.Property(t => t.FineCollected).HasColumnName("FineCollected");
            this.Property(t => t.SubmittedBy).HasColumnName("SubmittedBy");
            this.Property(t => t.Mobile).HasColumnName("Mobile");

            // Relationships
            this.HasOptional(t => t.BookIssue)
                .WithMany(t => t.BookReturns)
                .HasForeignKey(d => d.BookIssueId);

        }
    }
}
