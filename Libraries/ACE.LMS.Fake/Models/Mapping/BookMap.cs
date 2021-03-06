using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Fake.Models.Mapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Author)
                .IsRequired()
                .HasMaxLength(512);

            this.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Publication)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Books");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Publication).HasColumnName("Publication");
            this.Property(t => t.IsNewRequest).HasColumnName("IsNewRequest");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.TotalBooks).HasColumnName("TotalBooks");
            this.Property(t => t.ApprovedBooks).HasColumnName("ApprovedBooks");
            this.Property(t => t.NewApprovedBooks).HasColumnName("NewApprovedBooks");
            this.Property(t => t.IssuedBooks).HasColumnName("IssuedBooks");
            this.Property(t => t.TornBooks).HasColumnName("TornBooks");
            this.Property(t => t.AvailableBooks).HasColumnName("AvailableBooks");

            // Relationships
            this.HasOptional(t => t.AspNetUser)
                .WithMany(t => t.Books)
                .HasForeignKey(d => d.CreatedBy);

        }
    }
}
