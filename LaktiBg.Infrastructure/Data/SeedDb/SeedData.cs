using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;

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

        public ApplicationUser AdminUser { get; set; } = null!;
        public ApplicationUser NormalUser { get; set; } = null!;

        public Place Happy {  get; set; } = null!;

        public Place VilaPetra { get; set; } = null!;

        public Place CinemaCity { get; set; } = null!;


        public SeedData()
        {
            SeedEventTypes();
            SeedUsers();
            SeedPlaces();
        }

        private void SeedPlaces()
        {
            Happy = new Place
            {
                Id = 42,
                Name = "Happy Bar & Grill",
                Address = "ул. „Златю Бояджиев“ 2, 4000 Пловдив",
                OwnerId = "539e62e9-7926-446b-8d9c-92cd370dfde8",
                Contact = "0700 20 888",
                IsPublic = true,
                Rating = 5M,
                IsApproved = true,
            };

            VilaPetra = new Place
            {
                Id = 43,
                Name = "Вила Петра",
                Address = "Свинова поляна, 5641, град Априлци",
                OwnerId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                Contact = "+359878655666",
                IsPublic = true,
                Rating = 5M,
                IsApproved = true,
            };

            CinemaCity = new Place
            {
                Id = 44,
                Name = "Cinema City Пловдив",
                Address = "Западна промишлена зонаЗападен, ул. „Перущица“ 8, 4002 Пловдив",
                OwnerId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                Contact = "032 273 000",
                IsPublic = true,
                Rating = 5M,
                IsApproved = true,
            };

        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            AdminUser = new ApplicationUser()
            {
                Id = "539e62e9-7926-446b-8d9c-92cd370dfde8",
                UserName = "admin@abv.bg",
                NormalizedUserName = "ADMIN@ABV.BG",
                Email = "admin@abv.bg",
                NormalizedEmail = "ADMIN@ABV.BG",
                FirstName = "Miroslav",
                LastName = "Atanasov",
                Rating = 7M,
                Description = "Admin account",


            };

            AdminUser.PasswordHash =
                hasher.HashPassword(AdminUser, "admin123");

            NormalUser = new ApplicationUser()
            {
                Id = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                UserName = "normaluser@abv.bg",
                NormalizedUserName = "NORMALUSER@ABV.BG",
                Email = "normaluser@abv.bg",
                NormalizedEmail = "NORMALUSER@ABV.BG",
                FirstName = "Ivan",
                LastName = "Antonov",
                Rating = 5M,
                Description = "Hi! I am an normal user account!",
                PhoneNumber = "1234567890",

            };

            NormalUser.PasswordHash = 
                hasher.HashPassword(NormalUser, "#Pass1!");
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
