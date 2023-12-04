using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToneVault.Web.Areas.Identity.Data.ToneVault;

namespace ToneVault.Web.Areas.Identity.Data;

public class ApplicationUserEntityConfig : IEntityTypeConfiguration<WebUser>
{
    public void Configure(EntityTypeBuilder<WebUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}