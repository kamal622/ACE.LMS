using ACE.LMS.Core.Models.Library;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace ACE.LMS.Data.Models.Mapping.Library
{
    public class NoticeBoardMap : EntityTypeConfiguration<NoticeBoard>
    {
        public NoticeBoardMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(512);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(2056);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("NoticeBoard");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
