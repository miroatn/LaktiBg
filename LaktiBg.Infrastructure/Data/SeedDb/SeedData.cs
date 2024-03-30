using LaktiBg.Infrastructure.Data.Models;

namespace LaktiBg.Infrastructure.Data.SeedDb
{
    internal class SeedData
    {

        public EventType AlcoholType { get; set; } = null!;
        public EventType VeganType { get; set; } = null!;
        public EventType PartyType { get; set; } = null!;
        public EventType MeatType { get; set; } = null!;
        public EventType HikingType { get; set; } = null!;
        public EventType SmokingType { get; set; } = null!;
        public EventType LoudMusic { get; set; } = null!;
        public EventType MovieType { get; set; } = null!;
        public EventType VegetarianType { get; set; } = null!;
        public EventType GuesthouseType { get; set; } = null!;
        public EventType HomePartyType { get; set; } = null!;
        public EventType RestaurantType { get; set; } = null!;
        public EventType HutType { get; set; } = null!;


        public SeedData()
        {
            SeedEventTypes();
        }

        private void SeedEventTypes()
        {
            AlcoholType = new EventType()
            {
                Id = 1,
                Name = "Алкохол"
            };

            VeganType = new EventType()
            {
                Id = 2,
                Name = "Веган"
            };


            PartyType = new EventType()
            {
                Id = 3,
                Name = "Парти"
            };

            MeatType = new EventType()
            {
                Id = 4,
                Name = "Месо"
            };

            HikingType = new EventType()
            {
                Id = 5,
                Name = "Tуризъм"
            };

            SmokingType = new EventType()
            {
                Id = 6,
                Name = "За пушачи"
            };

            LoudMusic = new EventType()
            {
                Id = 7,
                Name = "Силна музика"
            };

            MovieType = new EventType()
            {
                Id = 8,
                Name = "Филм"
            };

            VegetarianType = new EventType()
            {
                Id = 9,
                Name = "Вегетарианско"
            };

            GuesthouseType = new EventType()
            {
                Id = 10,
                Name = "Къща за гости"
            };

            HomePartyType = new EventType()
            {
                Id = 11,
                Name = "Домашно парти"
            };

            RestaurantType = new EventType()
            {
                Id = 12,
                Name = "Ресторант"
            };

            HutType = new EventType()
            {
                Id = 13,
                Name = "Хижа"
            };
        }
    }
}
