using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Fake.Models.Mapping
{
    public class LibraryBookMap : EntityTypeConfiguration<LibraryBook>
    {
        public LibraryBookMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.BookNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AddedBY)
                .HasMaxLength(128);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(128);

            this.Property(t => t.RemovedBy)
                .HasMaxLength(128);

            this.Property(t => t.ReasonForRemoving)
                .HasMaxLength(250);

            this.Property(t => t.Notes)
                .HasMaxLength(250);

            this.Property(t => t.AccessionNo)
                .HasMaxLength(50);

            this.Property(t => t.ISBN)
                .HasMaxLength(32);

            this.Property(t => t.Store)
                .HasMaxLength(1028);

            // Table & Column Mappings
            this.ToTable("LibraryBooks");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BookId).HasColumnName("BookId");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.PurchaseDate).HasColumnName("PurchaseDate");
            this.Property(t => t.PublishedYear).HasColumnName("PublishedYear");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.BookNo).HasColumnName("BookNo");
            this.Property(t => t.ClassNo).HasColumnName("ClassNo");
            this.Property(t => t.AddedBY).HasColumnName("AddedBY");
            this.Property(t => t.AddedOn).HasColumnName("AddedOn");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.RemovedBy).HasColumnName("RemovedBy");
            this.Property(t => t.RemovedOn).HasColumnName("RemovedOn");
            this.Property(t => t.ReasonForRemoving).HasColumnName("ReasonForRemoving");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.HasCD).HasColumnName("HasCD");
            this.Property(t => t.AccessionNo).HasColumnName("AccessionNo");
            this.Property(t => t.IsVerified).HasColumnName("IsVerified");
            this.Property(t => t.Pages).HasColumnName("Pages");
            this.Property(t => t.ISBN).HasColumnName("ISBN");
            this.Property(t => t.Store).HasColumnName("Store");

            // Relationships
            this.HasOptional(t => t.Book)
                .WithMany(t => t.LibraryBooks)
                .HasForeignKey(d => d.BookId);

        }
    }
}
