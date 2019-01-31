using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Fake.Models.Mapping
{
    public class BookRequestHistoryMap : EntityTypeConfiguration<BookRequestHistory>
    {
        public BookRequestHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CreatedBy)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("BookRequestHistory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BookRequestDetailId).HasColumnName("BookRequestDetailId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");

            // Relationships
            this.HasOptional(t => t.AspNetUser)
                .WithMany(t => t.BookRequestHistories)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.BookRequestDetail)
                .WithMany(t => t.BookRequestHistories)
                .HasForeignKey(d => d.BookRequestDetailId);

        }
    }
}
