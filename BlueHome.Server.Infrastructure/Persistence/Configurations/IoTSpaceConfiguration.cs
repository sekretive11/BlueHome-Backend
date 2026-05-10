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
    public class IoTSpaceConfiguration : IEntityTypeConfiguration<IoTSpace>
    {
        public void Configure(EntityTypeBuilder<IoTSpace> builder)
        {
            builder.ToTable("iot_space");

            builder.HasKey(x => x.SpaceId);

            builder.Property(x => x.SpaceId)
                .HasColumnName("space_id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.SpaceName)
                .HasColumnName("space_name")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.SpaceType)
                .HasColumnName("space_type")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .HasDefaultValue(SpaceStatus.active)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasMany(x => x.Devices)
                .WithOne(x => x.Space)
                .HasForeignKey(x => x.SpaceId);

            builder.HasMany(x => x.UserLogs)
                .WithOne(x => x.Space)
                .HasForeignKey(x => x.SpaceId);
        }
    }
}
