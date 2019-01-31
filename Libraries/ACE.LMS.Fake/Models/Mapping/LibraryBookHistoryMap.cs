using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Fake.Models.Mapping
{
    public class LibraryBookHistoryMap : EntityTypeConfiguration<LibraryBookHistory>
    {
        public LibraryBookHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CreatedBy)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("LibraryBookHistory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LibraryBookId).HasColumnName("LibraryBookId");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");

            // Relationships
            this.HasOptional(t => t.AspNetUser)
                .WithMany(t => t.LibraryBookHistories)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.LibraryBook)
                .WithMany(t => t.LibraryBookHistories)
                .HasForeignKey(d => d.LibraryBookId);

        }
    }
}
