using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Configuration
{
    public class WorkerServiceConfiguration : IEntityTypeConfiguration<WorkerService>
    {
        public void Configure(EntityTypeBuilder<WorkerService> builder)
        {
        }
    }
}
