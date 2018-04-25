using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymTracker.Models
{
    public class ApplicationUserConfiguration
    {
        public ApplicationUserConfiguration(EntityTypeBuilder<ApplicationUser> entity)
        {
            entity.ToTable("ApplicationUser", "dbo");
            entity.HasKey(e => e.Id);
        }
    }
}