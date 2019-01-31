using ACE.LMS.Core.Models.Library;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Data.Models.Mapping.Library
{
    public class BooksInventoryMap : EntityTypeConfiguration<BooksInventory>
    {
        public BooksInventoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("BooksInventory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BookId).HasColumnName("BookId");
            this.Property(t => t.Total).HasColumnName("Total");
            this.Property(t => t.Available).HasColumnName("Available");

            // Relationships
            //this.HasRequired(t => t.Book)
            //    .WithMany(t => t.BooksInventories)
            //    .HasForeignKey(d => d.BookId);

        }
    }
}
