using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Infrastructure.Data.SeedDb
{
    internal class EventConnectionConfiguration : IEntityTypeConfiguration<EventTypeConnection>
    {
        public void Configure(EntityTypeBuilder<EventTypeConnection> builder)
        {
            var data = new SeedData();

            builder.HasData(new EventTypeConnection[] { data.HappyEventFirstConnection, data.HappyEventSecondConnection, data.HappyEventThirdConnection,
                data.VilaPetraEventFirstConnection, data.VilaPetraSecondConnection, data.VilaPetraThirdConnection,
                data.CinemaCityEventFirstConnection});
        }

    }
}
