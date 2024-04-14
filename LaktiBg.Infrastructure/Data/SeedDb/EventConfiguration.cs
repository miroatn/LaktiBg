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
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            var data = new SeedData();

            builder.HasData(new Event[] { data.CinemaCityEvent, 
                data.HappyEvent, data.VilaPetraEvent });
        }
    
    }
}
