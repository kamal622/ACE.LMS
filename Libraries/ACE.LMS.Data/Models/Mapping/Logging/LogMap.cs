using ACE.LMS.Core.Models.Logging;
using System.Data.Entity.ModelConfiguration;

namespace ACE.LMS.Data.Models.Mapping.Logging
{
    class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            this.ToTable("Log");
            this.HasKey(l => l.Id);
            this.Property(l => l.ShortMessage).IsRequired();
            this.Property(l => l.IpAddress).HasMaxLength(200);
            this.Ignore(l => l.LogLevel);
        }
    }
}
