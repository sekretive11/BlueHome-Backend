using BlueHome.Server.Domain.Entities;
using BlueHome.Server.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Persistence.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("devices");

            builder.HasKey(x => x.DeviceId);

            builder.Property(x => x.DeviceId)
                .HasColumnName("device_id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.SpaceId)
                .HasColumnName("space_id")
                .IsRequired();

            builder.Property(x => x.LocationId)
                .HasColumnName("location_id")
                .IsRequired();

            builder.Property(x => x.DeviceName)
                .HasColumnName("device_name")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.DeviceType)
                .HasColumnName("device_type")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .HasDefaultValue(DeviceStatus.online)
                .IsRequired();

            builder.HasOne(x => x.Space)
                .WithMany(x => x.Devices)
                .HasForeignKey(x => x.SpaceId);

            builder.HasOne(x => x.Location)
                .WithMany(x => x.Devices)
                .HasForeignKey(x => x.LocationId);

            builder.HasMany(x => x.EventLogs)
                .WithOne(x => x.Device)
                .HasForeignKey(x => x.DeviceId);
        }
    }
}
