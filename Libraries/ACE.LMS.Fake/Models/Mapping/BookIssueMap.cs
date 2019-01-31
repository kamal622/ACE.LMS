using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Fake.Models.Mapping
{
    public class BookIssueMap : EntityTypeConfiguration<BookIssue>
    {
        public BookIssueMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Notes)
                .HasMaxLength(150);

            this.Property(t => t.IssuedBy)
                .HasMaxLength(128);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(128);

            this.Property(t => t.BookReceivedBy)
                .HasMaxLength(100);

            this.Property(t => t.ReceiverNo)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("BookIssue");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BookRequestDetailsId).HasColumnName("BookRequestDetailsId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.LibraryBooksId).HasColumnName("LibraryBooksId");
            this.Property(t => t.IssuedBy).HasColumnName("IssuedBy");
            this.Property(t => t.IssueDate).HasColumnName("IssueDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.ReturnOnOrBefore).HasColumnName("ReturnOnOrBefore");
            this.Property(t => t.BookReceivedBy).HasColumnName("BookReceivedBy");
            this.Property(t => t.ReceiverNo).HasColumnName("ReceiverNo");

            // Relationships
            this.HasOptional(t => t.BookRequestDetail)
                .WithMany(t => t.BookIssues)
                .HasForeignKey(d => d.BookRequestDetailsId);
            this.HasOptional(t => t.LibraryBook)
                .WithMany(t => t.BookIssues)
                .HasForeignKey(d => d.LibraryBooksId);

        }
    }
}
