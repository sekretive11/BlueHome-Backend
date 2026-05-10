using BlueHome.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Persistence.Configurations
{
    public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            builder.ToTable("event_logs");

            builder.HasKey(x => x.EventLogId);

            builder.Property(x => x.EventLogId)
                .HasColumnName("event_log_id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DeviceId)
                .HasColumnName("device_id")
                .IsRequired();

            builder.Property(x => x.EventType)
                .HasColumnName("event_type")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(x => x.Device)
                .WithMany(x => x.EventLogs)
                .HasForeignKey(x => x.DeviceId);
        }
    }
}
