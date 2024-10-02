using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class WorkerServiceConfiguration : IEntityTypeConfiguration<WorkerService>
    {
        public void Configure(EntityTypeBuilder<WorkerService> builder)
        {
        }
    }
}
