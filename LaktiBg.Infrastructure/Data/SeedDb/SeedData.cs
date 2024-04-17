using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

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

        public Event HappyEvent { get; set; } = null!;
        public Event VilaPetraEvent { get; set; } = null!;
        public Event CinemaCityEvent { get; set; } = null!;

        public EventTypeConnection HappyEventFirstConnection { get; set; } = null!;
        public EventTypeConnection HappyEventSecondConnection { get; set; } = null!;
        public EventTypeConnection HappyEventThirdConnection { get; set; } = null!;

        public EventTypeConnection VilaPetraEventFirstConnection { get; set; } = null!;
        public EventTypeConnection VilaPetraSecondConnection { get; set; } = null!;
        public EventTypeConnection VilaPetraThirdConnection { get; set; } = null!;

        public EventTypeConnection CinemaCityEventFirstConnection { get; set; } = null!;

        public UsersEvents HappyEventFirstUser { get; set; } = null!;
        public UsersEvents HappyEventSecondUser { get; set; } = null!;

        public UsersEvents VilaPetraFirstUser { get; set; } = null!;

        public UsersEvents CinemaCityFirstUser { get; set; } = null!;

        public Comment HappyEventFirstComment { get; set; } = null!;
        public Comment HappyEventSecondComment { get; set; } = null!;
        public Comment HappyEventThirdComment { get; set; } = null!;

        public Comment VilaPetraEventFirstComment { get; set; } = null!;
        public Comment VilaPetraEventSecondComment { get; set; } = null!;
        public Comment CinemaCityEventFirstComment { get; set; } = null!;

        public UserFriends UserFriends { get; set; } = null!;
        public UserFriends FriendUserFriends { get; set; } = null!;

        public SeedData()
        {
            SeedEventTypes();
            SeedUsers();
            SeedPlaces();
            SeedEvents();
            SeedEventTypeConnections();
            SeedUsersEvents();
            SeedComments();
            SeedUserFriends();
        }

        private void SeedUserFriends()
        {
            UserFriends = new UserFriends()
            {
                Id = 1,
                UserId = "539e62e9-7926-446b-8d9c-92cd370dfde8",
                UserFriendId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                IsAccepted = true,
            };

            FriendUserFriends = new UserFriends()
            {
                Id = 2,
                UserId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                UserFriendId = "539e62e9-7926-446b-8d9c-92cd370dfde8",
                IsAccepted = true,
            };
        }

        private void SeedComments()
        {
            HappyEventFirstComment = new Comment
            {
                Id = 42,
                Text = "Излезе ли новото меню?",
                AuthorId = "539e62e9-7926-446b-8d9c-92cd370dfde8",
                EventId = 50,
                DateTime = DateTime.Parse("2024-04-11 14:22:00", CultureInfo.InvariantCulture),

            };

            HappyEventSecondComment = new Comment
            {
                Id = 43,
                Text = "Да, много е добро!",
                AuthorId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                EventId = 50,
                DateTime = DateTime.Parse("2024-04-11 17:45:00", CultureInfo.InvariantCulture),

            };

            HappyEventThirdComment = new Comment
            {
                Id = 44,
                Text = "Супер! Ще се видим там",
                AuthorId = "539e62e9-7926-446b-8d9c-92cd370dfde8",
                EventId = 50,
                DateTime = DateTime.Parse("2024-04-11 20:01:00", CultureInfo.InvariantCulture),

            };

            VilaPetraEventFirstComment = new Comment
            {
                Id = 45,
                Text = "Къщата има ли басейн?",
                AuthorId = "539e62e9-7926-446b-8d9c-92cd370dfde8",
                EventId = 51,
                DateTime = DateTime.Parse("2024-04-12 20:22:00", CultureInfo.InvariantCulture),
            };

            VilaPetraEventSecondComment = new Comment
            {
                Id = 46,
                Text = "Не, в съседната къща има и може да се ползва, тъй като е на същите собственици.",
                AuthorId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                EventId = 51,
                DateTime = DateTime.Parse("2024-04-12 22:10:24", CultureInfo.InvariantCulture),
            };

            CinemaCityEventFirstComment = new Comment
            {
                Id = 47,
                Text = "Ще закъснея малко.",
                AuthorId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                EventId = 52,
                DateTime = DateTime.Parse("2024-04-12 10:10:24", CultureInfo.InvariantCulture),
            };
        }

        private void SeedUsersEvents()
        {
            HappyEventFirstUser = new UsersEvents
            {
                UserId = "539e62e9-7926-446b-8d9c-92cd370dfde8",
                EventId = 50
            };

            HappyEventSecondUser = new UsersEvents
            {
                UserId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                EventId = 50
            };

            VilaPetraFirstUser = new UsersEvents
            {
                UserId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                EventId = 51
            };

            CinemaCityFirstUser = new UsersEvents
            {
                UserId = "539e62e9-7926-446b-8d9c-92cd370dfde8",
                EventId = 52
            };

        }

        private void SeedEventTypeConnections()
        {
            HappyEventFirstConnection = new EventTypeConnection
            {
                EventId = 50,
                EventTypeId = 1
            };

            HappyEventSecondConnection = new EventTypeConnection
            {
                EventId = 50,
                EventTypeId = 3,
            };

            HappyEventThirdConnection = new EventTypeConnection
            {
                EventId = 50,
                EventTypeId = 12
            };

            VilaPetraEventFirstConnection = new EventTypeConnection
            {
                EventId = 51,
                EventTypeId = 1,
            };

            VilaPetraSecondConnection = new EventTypeConnection
            {
                EventId = 51,
                EventTypeId = 3,
            };

            VilaPetraThirdConnection = new EventTypeConnection
            {
                EventId = 51,
                EventTypeId = 10,
            };

            CinemaCityEventFirstConnection = new EventTypeConnection
            {
                EventId = 52,
                EventTypeId = 8,
            };
        }

        private void SeedEvents()
        {
            HappyEvent = new Event()
            {
                Id = 50,
                Name = "Хапване на Хепи",
                CreationDate = DateTime.Now,
                StartDate = DateTime.Parse("2024-05-01 20:00:00", CultureInfo.InvariantCulture),
                PlaceId = 42,
                IsPublic = true,
                IsVisible = true,
                IsFinished = false,
                IsDeleted = false,
                OrganizerId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                MinRatingRequired = 3,
                ParticipantsMaxCount = 10,
                Description = "Смятам да почерпя по случай взетия изпит, не приемам не за отговор!"
            };

            VilaPetraEvent = new Event()
            {
                Id = 51,
                Name = "Събиране по случай петорния рожден ден",
                CreationDate = DateTime.Now,
                StartDate = DateTime.Parse("2024-11-08 14:00:00", CultureInfo.InvariantCulture),
                PlaceId = 43,
                IsPublic = true,
                IsVisible = true,
                IsFinished = false,
                IsDeleted = false,
                OrganizerId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                MinRatingRequired = 3,
                ParticipantsMaxCount = 29,
                Description = "Пет рожденника ще почерпим за рожденните дни, партито започва в 2 на обяд в петък и приключва в неделя. Нощувките се поемат от рождениците"
            };

            CinemaCityEvent = new Event()
            {
                Id = 52,
                Name = "Дюн 2",
                CreationDate = DateTime.Now,
                StartDate = DateTime.Parse("2024-03-28 20:00:00", CultureInfo.InvariantCulture),
                PlaceId = 44,
                IsPublic = true,
                IsVisible = true,
                IsFinished = true,
                IsDeleted = false,
                OrganizerId = "539e62e9-7926-446b-8d9c-92cd370dfde8",
                MinRatingRequired = 5,
                ParticipantsMaxCount = 5,
                Description = "Ще ходим до Пловдив да гледаме Дюн 2 в Cinema city с моята кола."
            };
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
