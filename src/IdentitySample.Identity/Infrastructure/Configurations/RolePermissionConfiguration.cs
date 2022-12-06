using IdentitySample.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentitySample.Identity.Infrastructure.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.OwnsOne(x => x.Permission, rp =>
        {
            rp.Property(p => p.Value).HasColumnName("PermissionValue");
            rp.Property(p => p.Key).HasColumnName("PermissionKey");
        });

        builder.HasOne(x => x.Role).WithMany(x => x.RolePermissions).HasForeignKey(x => x.RoleId);
    }
}
