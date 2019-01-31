using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ACE.LMS.Core.Models.Library;

namespace ACE.LMS.Data.Models.Mapping.Library
{
    public class CollegeMap : EntityTypeConfiguration<College>
    {
        public CollegeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Address)
                .HasMaxLength(1028);

            this.Property(t => t.City)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Colleges");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.HasBookBank).HasColumnName("HasBookBank");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
        }
    }
}
