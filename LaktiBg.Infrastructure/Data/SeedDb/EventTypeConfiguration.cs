using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaktiBg.Infrastructure.Data.SeedDb
{
    internal class EventTypeConfiguration : IEntityTypeConfiguration<EventType>
    {
        public void Configure(EntityTypeBuilder<EventType> builder)
        {
            var data = new SeedData();

            builder.HasData(new EventType[] {data.MeatType, data.MovieType, data.SmokingType,
            data.HutType, data.HikingType, data.AlcoholType, data.GuesthouseType, data.PartyType,
            data.HomePartyType, data.LoudMusic, data.VeganType, data.VegetarianType, data.RestaurantType });
        }
    }
}
