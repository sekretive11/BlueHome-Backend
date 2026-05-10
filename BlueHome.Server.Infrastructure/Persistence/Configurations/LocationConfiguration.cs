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
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("locations");

            builder.HasKey(x => x.LocationId);

            builder.Property(x => x.LocationId)
                .HasColumnName("location_id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.LocationName)
                .HasColumnName("location_name")
                .HasMaxLength(15)
                .IsRequired();

            builder.HasMany(x => x.Devices)
                .WithOne(x => x.Location)
                .HasForeignKey(x => x.LocationId);
        }
    }
}
