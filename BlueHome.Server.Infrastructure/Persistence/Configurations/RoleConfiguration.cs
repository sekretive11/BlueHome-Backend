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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("user_role");

            builder.HasKey(x => x.RoleId);

            builder.Property(x => x.RoleId)
                .HasColumnName("role_id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.RoleName)
                .HasColumnName("role_name")
                .HasMaxLength(13)
                .IsRequired();

            builder.HasIndex(x => x.RoleName).IsUnique();
        }
    }
}
