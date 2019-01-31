using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Fake.Models.Mapping
{
    public class NewBookRequestDetailMap : EntityTypeConfiguration<NewBookRequestDetail>
    {
        public NewBookRequestDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Subject)
                .HasMaxLength(128);

            this.Property(t => t.BookTitle)
                .IsRequired()
                .HasMaxLength(512);

            this.Property(t => t.Publication)
                .HasMaxLength(128);

            this.Property(t => t.Author)
                .HasMaxLength(128);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(16);

            // Table & Column Mappings
            this.ToTable("NewBookRequestDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BookRequestId).HasColumnName("BookRequestId");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.BookTitle).HasColumnName("BookTitle");
            this.Property(t => t.Publication).HasColumnName("Publication");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.BookRequest)
                .WithMany(t => t.NewBookRequestDetails)
                .HasForeignKey(d => d.BookRequestId);

        }
    }
}
