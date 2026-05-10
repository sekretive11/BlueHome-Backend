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
    public class UserLogConfiguration : IEntityTypeConfiguration<UserLog>
    {
        public void Configure(EntityTypeBuilder<UserLog> builder)
        {
            builder.ToTable("users_logs");

            builder.HasKey(x => x.UsersLogId);

            builder.Property(x => x.UsersLogId)
                .HasColumnName("users_log_id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(x => x.SpaceId)
                .HasColumnName("space_id")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserLogs)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Space)
                .WithMany(x => x.UserLogs)
                .HasForeignKey(x => x.SpaceId);
        }
    }
}
