using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Fake.Models.Mapping
{
    public class BookRequestDetailMap : EntityTypeConfiguration<BookRequestDetail>
    {
        public BookRequestDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Subject)
                .HasMaxLength(250);

            this.Property(t => t.Notes)
                .HasMaxLength(150);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("BookRequestDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RequestId).HasColumnName("RequestId");
            this.Property(t => t.BookId).HasColumnName("BookId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.BookAssigned).HasColumnName("BookAssigned");
            this.Property(t => t.AvailableFrom).HasColumnName("AvailableFrom");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");

            // Relationships
            this.HasOptional(t => t.AspNetUser)
                .WithMany(t => t.BookRequestDetails)
                .HasForeignKey(d => d.UpdatedBy);
            this.HasRequired(t => t.BookRequest)
                .WithMany(t => t.BookRequestDetails)
                .HasForeignKey(d => d.RequestId);
            this.HasRequired(t => t.Book)
                .WithMany(t => t.BookRequestDetails)
                .HasForeignKey(d => d.BookId);
            this.HasOptional(t => t.LibraryBook)
                .WithMany(t => t.BookRequestDetails)
                .HasForeignKey(d => d.BookAssigned);

        }
    }
}
