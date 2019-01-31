using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.LMS.Core;

namespace ACE.LMS.Data
{
    public abstract class BaseEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseEntityTypeConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.Property(t => t.Id).HasColumnName("Id");

        }
    }
}
