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
    internal class UsersEventsConfiguration : IEntityTypeConfiguration<UsersEvents>
    {
        public void Configure(EntityTypeBuilder<UsersEvents> builder)
        {
            var data = new SeedData();

            builder.HasData(new UsersEvents[] { data.HappyEventFirstUser, data.HappyEventSecondUser, 
                data.VilaPetraFirstUser, data.CinemaCityFirstUser });
        }

    }
}
