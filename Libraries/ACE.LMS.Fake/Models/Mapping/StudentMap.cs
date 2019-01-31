using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Fake.Models.Mapping
{
    public class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.EnrollmentNo)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.LastName)
                .HasMaxLength(128);

            this.Property(t => t.FatherName)
                .HasMaxLength(128);

            this.Property(t => t.MotherName)
                .HasMaxLength(128);

            this.Property(t => t.FatherNative)
                .HasMaxLength(128);

            this.Property(t => t.MotherNative)
                .HasMaxLength(128);

            this.Property(t => t.PresentAddress)
                .HasMaxLength(2056);

            this.Property(t => t.PermanentAddress)
                .HasMaxLength(2056);

            this.Property(t => t.Mobile)
                .HasMaxLength(16);

            this.Property(t => t.Phone)
                .HasMaxLength(16);

            this.Property(t => t.Email)
                .HasMaxLength(1028);

            this.Property(t => t.SSCResult)
                .HasMaxLength(32);

            this.Property(t => t.HSCResult)
                .HasMaxLength(32);

            this.Property(t => t.ReferenceUserId)
                .HasMaxLength(128);

            this.Property(t => t.OtherReference)
                .HasMaxLength(128);

            this.Property(t => t.FatherOccupation)
                .HasMaxLength(128);

            this.Property(t => t.ImagePath)
                .HasMaxLength(512);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(128);

            this.Property(t => t.ReferenceMobile)
                .HasMaxLength(15);

            this.Property(t => t.AlternateEmail)
                .IsRequired()
                .HasMaxLength(1028);

            this.Property(t => t.Note)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Students");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.EnrollmentNo).HasColumnName("EnrollmentNo");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.FatherName).HasColumnName("FatherName");
            this.Property(t => t.MotherName).HasColumnName("MotherName");
            this.Property(t => t.FatherNative).HasColumnName("FatherNative");
            this.Property(t => t.MotherNative).HasColumnName("MotherNative");
            this.Property(t => t.PresentAddress).HasColumnName("PresentAddress");
            this.Property(t => t.PermanentAddress).HasColumnName("PermanentAddress");
            this.Property(t => t.CityId).HasColumnName("CityId");
            this.Property(t => t.DOB).HasColumnName("DOB");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.SSCResult).HasColumnName("SSCResult");
            this.Property(t => t.SSCPassingYear).HasColumnName("SSCPassingYear");
            this.Property(t => t.HSCResult).HasColumnName("HSCResult");
            this.Property(t => t.HSCPassingYear).HasColumnName("HSCPassingYear");
            this.Property(t => t.ReferenceUserId).HasColumnName("ReferenceUserId");
            this.Property(t => t.OtherReference).HasColumnName("OtherReference");
            this.Property(t => t.FatherOccupation).HasColumnName("FatherOccupation");
            this.Property(t => t.InHostel).HasColumnName("InHostel");
            this.Property(t => t.ImagePath).HasColumnName("ImagePath");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedOn).HasColumnName("UpdatedOn");
            this.Property(t => t.ReferenceMobile).HasColumnName("ReferenceMobile");
            this.Property(t => t.FamilyYearlyIncome).HasColumnName("FamilyYearlyIncome");
            this.Property(t => t.NoOfBrotherSis).HasColumnName("NoOfBrotherSis");
            this.Property(t => t.IsLike).HasColumnName("IsLike");
            this.Property(t => t.IsLikeContribution).HasColumnName("IsLikeContribution");
            this.Property(t => t.IsLikeBookShare).HasColumnName("IsLikeBookShare");
            this.Property(t => t.IsLikeVoluntary).HasColumnName("IsLikeVoluntary");
            this.Property(t => t.IsEligible).HasColumnName("IsEligible");
            this.Property(t => t.IsReferenceValidate).HasColumnName("IsReferenceValidate");
            this.Property(t => t.AlternateEmail).HasColumnName("AlternateEmail");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.Priority).HasColumnName("Priority");

            // Relationships
            this.HasRequired(t => t.AspNetUser)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.UserId);
            this.HasOptional(t => t.AspNetUser1)
                .WithMany(t => t.Students1)
                .HasForeignKey(d => d.ReferenceUserId);
            this.HasRequired(t => t.AspNetUser2)
                .WithMany(t => t.Students2)
                .HasForeignKey(d => d.CreatedBy);
            this.HasOptional(t => t.AspNetUser3)
                .WithMany(t => t.Students3)
                .HasForeignKey(d => d.UpdatedBy);
            this.HasRequired(t => t.City)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.CityId);

        }
    }
}
