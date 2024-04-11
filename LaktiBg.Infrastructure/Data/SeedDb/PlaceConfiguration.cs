using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Infrastructure.Data.SeedDb
{
    internal class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            var data = new SeedData();

            builder.HasData(new Place[] { data.CinemaCity, data.Happy, data.VilaPetra });
        }
    }
    
    
}
