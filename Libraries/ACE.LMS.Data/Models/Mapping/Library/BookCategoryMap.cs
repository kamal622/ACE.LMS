using ACE.LMS.Core.Models.Library;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Data.Models.Mapping.Library
{
    public class BookCategoryMap : EntityTypeConfiguration<BookCategory>
    {
        public BookCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("BookCategories");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BookId).HasColumnName("BookId");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");

            // Relationships
            this.HasRequired(t => t.Book)
                .WithMany(t => t.BookCategories)
                .HasForeignKey(d => d.BookId);
            this.HasRequired(t => t.Category)
                .WithMany(t => t.BookCategories)
                .HasForeignKey(d => d.CategoryId);

        }
    }
}
