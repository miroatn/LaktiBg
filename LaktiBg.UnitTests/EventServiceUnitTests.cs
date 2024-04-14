using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Enums;
using LaktiBg.Core.Models.EventModels;
using LaktiBg.Core.Services.EventServices;
using LaktiBg.Core.Services.UserServices;
using LaktiBg.Infrastructure.Data;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LaktiBg.UnitTests
{
    [TestFixture]
    public class EventServiceUnitTests
    {
        private ApplicationDbContext dbContext;
        private IEnumerable<ApplicationUser> users;
        private IEnumerable<Place> places;
        private IEnumerable<Event> events;
        private IEnumerable<UsersEvents> usersEvents;
        private IEnumerable<EventType> eventTypes;
        private IEnumerable<EventTypeConnection> eventTypeConnections;


        private IRepository repository;
        private IEventService service;

        //Users
        private ApplicationUser FirstUser;
        private ApplicationUser SecondUser;
        private ApplicationUser ThirdUser;

        //Events
        private Event FirstEvent;
        private Event SecondEvent;
        private Event ThirdEvent;
        private Event FourthEvent;
        private Event FifthEvent;
        private Event SixtEvent;

        //Places
        private Place Happy;
        private Place MiroHouse;
        private Place VilaPetra;
        private Place KinoArena;
        private Place CinemaCity;

        private UsersEvents FirstUserEvent;
        private UsersEvents SecondUserEvent;
        private UsersEvents ThirdUserEvent;
        private UsersEvents FourthUserEvent;

        //EventType

        private EventType Alchohol;
        private EventType Vegan;
        private EventType Party;

        //EventTypeConnections

        private EventTypeConnection FirstConnection;
        private EventTypeConnection SecondConnection;
        private EventTypeConnection ThirdConnection;


        [SetUp]

        public async Task SetUp()
        {

            //EventTypes

            Alchohol = new EventType()
            {
                Id = 1,
                Name = "Алкохол"
            };

            Vegan = new EventType()
            {
                Id= 2,
                Name = "Веган"
            };

            Party = new EventType()
            {
                Id = 3,
                Name = "Парти"
            };

            eventTypes = new List<EventType>()
            {
                Alchohol, Vegan, Party
            };

            //ApplicationUsers

            var hasher = new PasswordHasher<ApplicationUser>();

            FirstUser = new ApplicationUser()
            {
                Id = "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                FirstName = "Ivan",
                LastName = "Draganov",
                BirthDate = DateTime.Parse("2010-04-11", CultureInfo.InvariantCulture),
                Rating = 5,
                RegistrationDate = DateTime.Now,
                Address = "Ulica Sezam 42",
                Description = "Ivans description",
                UserName = "ivandraganov@abv.bg",
                NormalizedUserName = "IVANDRAGANOV@ABV.BG",
                Email = "ivandraganov@abv.bg",
                NormalizedEmail = "IVANDRAGANOV@ABV.BG",
                PhoneNumber = "1234567890",

            };

            FirstUser.PasswordHash = hasher.HashPassword(FirstUser, "Ivandraganov123*");

            SecondUser = new ApplicationUser()
            {
                Id = "fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                FirstName = "Georgi",
                LastName = "Georgiev",
                BirthDate = DateTime.Parse("2000-02-22", CultureInfo.InvariantCulture),
                Rating = 5,
                RegistrationDate = DateTime.Now,
                Address = "Chernorizec Hrabur 44",
                Description = "Georgi`s description",
                UserName = "gosho@abv.bg",
                NormalizedUserName = "GOSHO@ABV.BG",
                Email = "gosho@abv.bg",
                NormalizedEmail = "GOSHO@ABV.BG",
                PhoneNumber = "088888888",

            };

            SecondUser.PasswordHash = hasher.HashPassword(SecondUser, "goshoOo1*");

            ThirdUser = new ApplicationUser()
            {
                Id = "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                FirstName = "Petur",
                LastName = "Nikolov",
                BirthDate = DateTime.Parse("1992-10-10", CultureInfo.InvariantCulture),
                Rating = 3,
                RegistrationDate = DateTime.Now,
                Address = "Blatna livada 11",
                Description = "Petur`s description",
                UserName = "pe6o@abv.bg",
                NormalizedUserName = "Pe6O@ABV.BG",
                Email = "pe6o@abv.bg",
                NormalizedEmail = "PE6O@ABV.BG",
                PhoneNumber = "08987897867",

            };

            ThirdUser.PasswordHash = hasher.HashPassword(SecondUser, "goshoOo1*");

            users = new List<ApplicationUser>()
            {
                FirstUser, SecondUser, ThirdUser,
            };

            //Places

            Happy = new Place()
            {
                Id = 1,
                Name = "Happy Bar & Grill",
                OwnerId = "539e62e9-7926-446b-8d9c-92cd370dfde8",
                Contact = "0700 20 888",
                Address = "ул. „Златю Бояджиев“ 2, 4000 Пловдив",
                IsPublic = true,
                Rating = 5.00M,
                IsApproved = true,
            };

            MiroHouse = new Place()
            {
                Id = 2,
                Name = "Miro`s house",
                OwnerId = "2ed5c668-610f-41a9-8378-f648200ee2a1",
                Contact = "0883582178",
                Address = "Ala bala 21",
                IsPublic = true,
                Rating = 4.00M,
                IsApproved = true,
            };

            VilaPetra = new Place()
            {
                Id = 3,
                Name = "Vila Petra",
                OwnerId = "71368c9b-91fa-4338-bfce-e0921b5324ef",
                Contact = "+359878655666",
                Address = "Свинова поляна, 5641, град Априлци",
                IsPublic = true,
                Rating = 7.00M,
                IsApproved = true,
            };

            KinoArena = new Place()
            {
                Id = 4,
                Name = "Kino Arena",
                OwnerId = "2ed5c668-610f-41a9-8378-f648200ee2a1",
                Contact = "02 404 7125",
                Address = "Център Пловдив Център, бул. „Руски“ 54, 4000 Пловдив",
                IsPublic = false,
                Rating = 6.00M,
                IsApproved = true,
            };

            CinemaCity = new Place()
            {
                Id = 5,
                Name = "Cinema city",
                OwnerId = "2ed5c668-610f-41a9-8378-f648200ee2a1",
                Contact = "123456789",
                Address = "ул.„Перущица“ 8",
                IsPublic = true,
                Rating = 6.42M,
                IsApproved = false,
            };

            places = new List<Place>()
            {
                Happy, MiroHouse, CinemaCity, KinoArena, VilaPetra
            };

            //Events

            FirstEvent = new Event()
            {
                Id = 1,
                Name = "Хапване в Хепи",
                CreationDate = DateTime.Parse("2024-04-14", CultureInfo.InvariantCulture),
                StartDate = DateTime.Parse("2024-04-22", CultureInfo.InvariantCulture),
                PlaceId = 1,
                IsPublic = true,
                IsDeleted = false,
                IsVisible = true,
                IsFinished = false,
                OrganizerId = "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                MinRatingRequired = 4,
                ParticipantsMaxCount = 2,
                MinAgeRequired = 18,
                Description = "Да хапнем в Хепи за вечеря. Час на събиране : 20:00"

            };

            SecondEvent = new Event()
            {
                Id = 2,
                Name = "На домашно у Миро",
                CreationDate = DateTime.Parse("2024-03-09", CultureInfo.InvariantCulture),
                StartDate = DateTime.Parse("2024-03-22", CultureInfo.InvariantCulture),
                PlaceId = 2,
                IsPublic = true,
                IsDeleted = false,
                IsVisible = true,
                IsFinished = true,
                OrganizerId = "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                MinRatingRequired = 4,
                ParticipantsMaxCount = 2,
                MinAgeRequired = 10,
                Description = "Ще има пушено месо, носете бира!"

            };

            ThirdEvent = new Event()
            {
                Id = 3,
                Name = "Дюн 2",
                CreationDate = DateTime.Parse("2024-03-17", CultureInfo.InvariantCulture),
                StartDate = DateTime.Parse("2024-03-25", CultureInfo.InvariantCulture),
                PlaceId = 4,
                IsPublic = true,
                IsDeleted = false,
                IsVisible = true,
                IsFinished = true,
                OrganizerId = "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                MinRatingRequired = 1,
                ParticipantsMaxCount = 8,
                MinAgeRequired = 18,
                Description = "Да гледаме Дюн 2 тази седмица!"

            };

            FourthEvent = new Event()
            {
                Id = 4,
                Name = "Дюн 3",
                CreationDate = DateTime.Parse("2024-03-16", CultureInfo.InvariantCulture),
                StartDate = DateTime.Parse("2024-05-25", CultureInfo.InvariantCulture),
                PlaceId = 4,
                IsPublic = true,
                IsDeleted = false,
                IsVisible = true,
                IsFinished = false,
                OrganizerId = "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                MinRatingRequired = 3,
                ParticipantsMaxCount = 10,
                MinAgeRequired = 20,
                Description = "Да гледаме Дюн 3 тази седмица!"

            };

            FifthEvent = new Event()
            {
                Id = 5,
                Name = "Дюн 4",
                CreationDate = DateTime.Parse("2024-03-15", CultureInfo.InvariantCulture),
                StartDate = DateTime.Parse("2024-05-25", CultureInfo.InvariantCulture),
                PlaceId = 4,
                IsPublic = true,
                IsDeleted = false,
                IsVisible = true,
                IsFinished = false,
                OrganizerId = "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                MinRatingRequired = 2,
                ParticipantsMaxCount = 8,
                MinAgeRequired = 4,
                Description = "Да гледаме Дюн 4 тази седмица!"

            };

            SixtEvent = new Event()
            {
                Id = 6,
                Name = "Дюн 5",
                CreationDate = DateTime.Parse("2024-03-14", CultureInfo.InvariantCulture),
                StartDate = DateTime.Parse("2024-05-25", CultureInfo.InvariantCulture),
                PlaceId = 4,
                IsPublic = true,
                IsDeleted = false,
                IsVisible = true,
                IsFinished = false,
                OrganizerId = "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                MinRatingRequired = 2,
                ParticipantsMaxCount = 34,
                MinAgeRequired = 42,
                Description = "Да гледаме Дюн 5 тази седмица!"

            };

            events = new List<Event>()
            {
                FirstEvent, SecondEvent, ThirdEvent, FourthEvent, FifthEvent, SixtEvent
            };

            //UsersEvents

            FirstUserEvent = new UsersEvents()
            {
                UserId = "fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                EventId = 42,
            };

            SecondUserEvent = new UsersEvents()
            {
                UserId = "fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                EventId = 43,
            };

            ThirdUserEvent = new UsersEvents()
            {
                UserId = "fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                EventId = 44,
            };

            FourthUserEvent = new UsersEvents()
            {
                UserId = "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                EventId = 44,

            };

            usersEvents = new List<UsersEvents>()
            {
                FirstUserEvent, SecondUserEvent, ThirdUserEvent, FourthUserEvent
            };

            //EventTypeConnections

            FirstConnection = new EventTypeConnection()
            {
                EventId = 5,
                EventTypeId = 1
            };

            SecondConnection = new EventTypeConnection()
            {
                EventId = 4,
                EventTypeId = 2
            };

            ThirdConnection = new EventTypeConnection()
            {
                EventId = 6,
                EventTypeId = 3
            };

            eventTypeConnections = new List<EventTypeConnection>()
            {
                FirstConnection, SecondConnection, ThirdConnection,
            };



            //Database

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LaktiBgInMemoryDb")
                .Options;

            dbContext = new ApplicationDbContext(options);

            await dbContext.AddRangeAsync(users);
            await dbContext.AddRangeAsync(places);
            await dbContext.AddRangeAsync(events);
            await dbContext.AddRangeAsync(usersEvents);
            await dbContext.AddRangeAsync(eventTypes);
            await dbContext.AddRangeAsync(eventTypeConnections);
            await dbContext.SaveChangesAsync();

            var mockUserService = new Mock<IUserService>();

            mockUserService
                .Setup(x => x.GetUsersAgeById("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be"))
                .Returns(Task.FromResult(31));

            mockUserService
                .Setup(x => x.GetUsersRatingById("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be"))
                .Returns(Task.FromResult(3M));

            repository = new Repository(dbContext);
            service = new EventService(repository, mockUserService.Object);
        }

        [TearDown]

        public async Task Teardown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]

        public async Task Test_AddAsync_WorkingCorrectly()
        {
            EventFormModel model= new EventFormModel()
            {
                Name = "TestEvent",
                SelectedTypes = new List<int>() { 1,2,3 },
                StartDate = DateTime.Parse("2024-05-01", CultureInfo.InvariantCulture),
                SelectedPlaceId = 1,
                IsPublic = true,
                IsVisible = true,
                OrganizerId = "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                MinRatingRequired = 1,
                ParticipantsMaxCount = 10,
                MinAgeRequired = 18,
                Description = "With this model im testing the AddAsync method of Event entity."
            };

            await service.AddAsync(model);

            var newEvent = await service.GetEventByIdAsync(7);

            Assert.IsNotNull(newEvent);
            Assert.AreEqual("TestEvent", newEvent.Name);
            Assert.AreEqual("1.5.2024 г. 0:00:00", newEvent.StartDate.ToString());
            Assert.IsTrue(newEvent.IsPublic);
            Assert.IsTrue(newEvent.IsVisible);
            Assert.AreEqual("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be", newEvent.OrganizerId);
            Assert.AreEqual(1, newEvent.MinRatingRequired);
            Assert.AreEqual(10, newEvent.ParticipantsMaxCount);
            Assert.AreEqual("With this model im testing the AddAsync method of Event entity.", newEvent.Description);
        }

        [Test]

        public async Task Test_AddAsync_ShouldThrowExceptionWhenTypesAreNullOrNotFound()
        {
            EventFormModel model = new EventFormModel()
            {
                Name = "TestEvent",
                SelectedTypes = new List<int>() { 20},
                StartDate = DateTime.Parse("2024-05-01", CultureInfo.InvariantCulture),
                SelectedPlaceId = 1,
                IsPublic = true,
                IsVisible = true,
                OrganizerId = "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                MinRatingRequired = 1,
                ParticipantsMaxCount = 10,
                MinAgeRequired = 18,
                Description = "With this model im testing the AddAsync method of Event entity."
            };

            try
            {
                await service.AddAsync(model);
            }
            catch (Exception ex)
            {

               Assert.AreEqual("The eventTypes are not found", ex.Message);
            }
        }

        [Test]

        public async Task Test_AddAsync_ShouldThrowExceptionWhenPlaceIsNotValid()
        {
            EventFormModel model = new EventFormModel()
            {
                Name = "TestEvent",
                SelectedTypes = new List<int>() { 1, 2, 3 },
                StartDate = DateTime.Parse("2024-05-01", CultureInfo.InvariantCulture),
                SelectedPlaceId = 100,
                IsPublic = true,
                IsVisible = true,
                OrganizerId = "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                MinRatingRequired = 1,
                ParticipantsMaxCount = 10,
                MinAgeRequired = 18,
                Description = "With this model im testing the AddAsync method of Event entity."
            };

            try
            {
                await service.AddAsync(model);
            }
            catch (Exception ex)
            {

                Assert.AreEqual("Value cannot be null. (Parameter 'The place is not found')", ex.Message);
            }
        }

        [Test]

        public async Task Test_GetEventTypeConnection_ShouldReturnEventTypeConnectionByGivenId()
        {
            IEnumerable<int> ids = new List<int>() { 1, 2, 3 };
            var result = await service.GetEventTypeConnections(ids);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [Test]

        public async Task Test_GetEventTypeViewsAsync_ShouldReturnAllEventTypesFromDb()
        {
            var result = await service.GetEventTypeViewsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [Test]

        public async Task Test_GetPlaceByIdAsync_ShouldReturnThePlaceIfIdIsValid()
        {
            Place result = await service.GetPlaceByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Happy Bar & Grill", result.Name);
            Assert.AreEqual("539e62e9-7926-446b-8d9c-92cd370dfde8", result.OwnerId);
            Assert.AreEqual("0700 20 888", result.Contact);
            Assert.AreEqual("ул. „Златю Бояджиев“ 2, 4000 Пловдив", result.Address);
            Assert.AreEqual(5, result.Rating);
            Assert.IsTrue(result.IsApproved);

        }

        [Test]

        public async Task Test_GetPlacesViewsAsync_ReturnsAllPlacesEventModels()
        {
            var result = await service.GetPlacesViewsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count());

        }

        [Test]

        public async Task Test_AllAsync_ShouldReturnAllEventsThatAreVisibleNotDeletedArePublicAndIsNotFinished()
        {
            var result = await service.AllAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                null,
                null,
                EventSorting.Newest,
                1,
                10);

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Events.Count());
        }

        [Test]

        public async Task Test_AllAsync_SearchTermShouldWorkCorrectly()
        {
            var result = await service.AllAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
            null,
            "Дюн 3",
            EventSorting.Newest,
            1,
            10);

            var model = result.Events.First();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Events.Count());
            Assert.AreEqual("Дюн 3", model.Name);
        }

        [Test]

        public async Task Test_AllAsync_EventSortingLowestRatingShouldOrderEventsCorrectly()
        {
            var result = await service.AllAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                        null,
                        null,
                        EventSorting.LowestRating,
                        1,
                        10);

            int? lowestRating = result.Events.Select(e => e.MinRatingRequired).First();
            int? secondSorting = result.Events.Skip(1).Select(e => e.MinRatingRequired).First();
            int? thirdSorting = result.Events.Skip(2).Select(e => e.MinRatingRequired).First();
            int? forthSorting = result.Events.Skip(3).Select(e => e.MinRatingRequired).First();

            Assert.AreEqual(2, lowestRating);
            Assert.AreEqual(2, secondSorting);
            Assert.AreEqual(3, thirdSorting);
            Assert.AreEqual(4, forthSorting);
        }


        [Test]

        public async Task Test_AllAsync_EventSortingHighestRatingShouldOrderEventsCorrectly()
        {
            var result = await service.AllAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                        null,
                        null,
                        EventSorting.HighestRating,
                        1,
                        10);

            int? lowestRating = result.Events.Select(e => e.MinRatingRequired).First();
            int? secondSorting = result.Events.Skip(1).Select(e => e.MinRatingRequired).First();
            int? thirdSorting = result.Events.Skip(2).Select(e => e.MinRatingRequired).First();
            int? forthSorting = result.Events.Skip(3).Select(e => e.MinRatingRequired).First();

            Assert.AreEqual(4, lowestRating);
            Assert.AreEqual(3, secondSorting);
            Assert.AreEqual(2, thirdSorting);
            Assert.AreEqual(2, forthSorting);
        }

        [Test]

        public async Task Test_AllAsync_EventSortingOldestShouldOrderEventsCorrectly()
        {
            var result = await service.AllAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                        null,
                        null,
                        EventSorting.Oldest,
                        1,
                        10);

            int firstEventId = result.Events.Select(e => e.Id).First();
            int secondEventId = result.Events.Skip(1).Select(e => e.Id).First();
            int thirdEventId = result.Events.Skip(2).Select(e => e.Id).First();
            int forthEventId = result.Events.Skip(3).Select(e => e.Id).First();

            Assert.AreEqual(6, firstEventId);
            Assert.AreEqual(5, secondEventId);
            Assert.AreEqual(4, thirdEventId);
            Assert.AreEqual(1, forthEventId);

           
        }

        [Test]

        public async Task Test_AllAsync_EventSortingNewestShouldOrderEventsCorrectly()
        {
            var result = await service.AllAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                        null,
                        null,
                        EventSorting.Newest,
                        1,
                        10);

            int firstEventId = result.Events.Select(e => e.Id).First();
            int secondEventId = result.Events.Skip(1).Select(e => e.Id).First();
            int thirdEventId = result.Events.Skip(2).Select(e => e.Id).First();
            int forthEventId = result.Events.Skip(3).Select(e => e.Id).First();

            Assert.AreEqual(1, firstEventId);
            Assert.AreEqual(4, secondEventId);
            Assert.AreEqual(5, thirdEventId);
            Assert.AreEqual(6, forthEventId);
        }

        [Test]

        public async Task Test_AllAsync_EventSortingMostFilledOrderEventsCorrectly()
        {
            await service.ParticipateInEvent(5, "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");
            await service.ParticipateInEvent(5, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            await service.ParticipateInEvent(5, "fb4143db-57af-4ebe-950c-b17e1b3c4fb4");
            await service.ParticipateInEvent(6, "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");
            await service.ParticipateInEvent(6, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            await service.ParticipateInEvent(1, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            var result = await service.AllAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                        null,
                        null,
                        EventSorting.MostFilled,
                        1,
                        10);

            int firstEventId = result.Events.Select(e => e.Id).First();
            int secondEventId = result.Events.Skip(1).Select(e => e.Id).First();
            int thirdEventId = result.Events.Skip(2).Select(e => e.Id).First();
            int forthEventId = result.Events.Skip(3).Select(e => e.Id).First();

            Assert.AreEqual(5, firstEventId);
            Assert.AreEqual(6, secondEventId);
            Assert.AreEqual(1, thirdEventId);
            Assert.AreEqual(4, forthEventId);

        }

        [Test]

        public async Task Test_AllAsync_EventSortingLeastFilledOrderEventsCorrectly()
        {
            await service.ParticipateInEvent(5, "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");
            await service.ParticipateInEvent(5, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            await service.ParticipateInEvent(5, "fb4143db-57af-4ebe-950c-b17e1b3c4fb4");
            await service.ParticipateInEvent(6, "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");
            await service.ParticipateInEvent(6, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            await service.ParticipateInEvent(1, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            var result = await service.AllAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                        null,
                        null,
                        EventSorting.LeastFilled,
                        1,
                        10);

            int firstEventId = result.Events.Select(e => e.Id).First();
            int secondEventId = result.Events.Skip(1).Select(e => e.Id).First();
            int thirdEventId = result.Events.Skip(2).Select(e => e.Id).First();
            int forthEventId = result.Events.Skip(3).Select(e => e.Id).First();

            Assert.AreEqual(4, firstEventId);
            Assert.AreEqual(1, secondEventId);
            Assert.AreEqual(6, thirdEventId);
            Assert.AreEqual(5, forthEventId);

        }

        [Test]

        public async Task Test_GetUsersNameByIdAsync_ShouldReturnUsersFullNameIfUserIsValid()
        {
            var result = await service.GetUsersNameByIdAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4");

            Assert.IsNotNull(result);
            Assert.AreEqual("Georgi Georgiev", result);
        }

        [Test]

        public async Task Test_CheckEventById_ShouldReturnTrueIfTheresEventWithGivenId()
        {
            bool result = await service.CheckEventById(1);

            Assert.IsTrue(result);
        }

        [Test]

        public async Task Test_CheckEventById_ShouldReturnFalseIfTheresEventWithGivenId()
        {
            bool result = await service.CheckEventById(1001);

            Assert.IsFalse(result);
        }

        [Test]

        public async Task Test_ParticipateInEvent_ShouldAddParticipantToEvent()
        {
            var result = await service.GetEventByIdAsync(5);

            Assert.AreEqual(0, result.Participants.Count);

            await service.ParticipateInEvent(5, "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.AreEqual(1, result.Participants.Count);
        }

        [Test]

        public async Task Test_CheckIfUserIsAlreadyInEvent_ShouldReturnTrueIfUserIsAlreadyInEvent()
        {
            await service.ParticipateInEvent(5, "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            bool result = await service.CheckIfUserIsAlreadyInEvent(5, "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Test_CheckIfUserIsAlreadyInEvent_ShouldReturnFalseIfUserIsNotInTheEvent()
        {

            bool result = await service.CheckIfUserIsAlreadyInEvent(5, "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsFalse(result);
        }

        [Test]

        public async Task Test_GetEventViewModelByIdAsync_ShouldReturnEventViewModelByGivenId()
        {
            var model = await service.GetEventViewModelByIdAsync(5);

            Assert.IsNotNull(model);
            Assert.AreEqual(5, model.Id);
            Assert.AreEqual("Дюн 4", model.Name);
            Assert.AreEqual("25.05.2024 00:00", model.StartDate.ToString());
            Assert.AreEqual(4, model.Place.Id);
            Assert.IsTrue(model.IsPublic);
            Assert.IsFalse(model.IsDeleted);
            Assert.IsTrue(model.IsVisible);
            Assert.IsFalse(model.IsFinished);
            Assert.AreEqual("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386", model.OrganizerId);
            Assert.AreEqual(2, model.MinRatingRequired);
            Assert.AreEqual(8, model.ParticipantsMaxCount);
            Assert.AreEqual(4, model.MinAgeRequired);
            Assert.AreEqual("Да гледаме Дюн 4 тази седмица!", model.Description);
        }

        [Test]

        public async Task Test_DeleteAsync_DeleteEventWhenEventIdIsValid()
        {
            var result = await service.GetEventViewModelByIdAsync(1);

            Assert.IsNotNull(result);

            await service.DeleteAsync(1, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            var updatedResult = await service.GetEventViewModelByIdAsync(1);

            Assert.IsNull(updatedResult);
        }

        [Test]

        public async Task Test_DeleteAsync_ShouldThrowExceptionWhenUserIsNotTheOrganizer()
        {
            try
            {
                await service.DeleteAsync(1, "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");
            }
            catch (Exception ex) 
            {

                Assert.AreEqual("You dont have permitions for this action", ex.Message);
            }
        }

        [Test]

        public async Task Test_GetEventFormModelByIdAsync_ShouldReturnEventFormModelWhenGivenValidId()
        {
            var model = await service.GetEventFormModelByIdAsync(5);

            Assert.IsNotNull(model);
            Assert.AreEqual(5, model.Id);
            Assert.AreEqual("Дюн 4", model.Name);
            Assert.AreEqual("25.5.2024 г. 0:00:00", model.StartDate.ToString());;
            Assert.IsTrue(model.IsPublic);
            Assert.IsTrue(model.IsVisible);
            Assert.AreEqual("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386", model.OrganizerId);
            Assert.AreEqual(2, model.MinRatingRequired);
            Assert.AreEqual(8, model.ParticipantsMaxCount);
            Assert.AreEqual(4, model.MinAgeRequired);
            Assert.AreEqual("Да гледаме Дюн 4 тази седмица!", model.Description);
        }

        [Test]

        public async Task Test_EditAsync_EditTheEventWhenGivenValidId()
        {
            IEnumerable<int> selectedTypes = new List<int>() { 1 };

            EventFormModel model = new EventFormModel()
            {
                Id = 1,
                SelectedTypes = selectedTypes,
                Name = "Хапване в Хепи EDIT",
                StartDate = DateTime.Parse("2028-01-11", CultureInfo.InvariantCulture),
                SelectedPlaceId = 2,
                IsPublic = true,
                IsVisible = true,
                MinRatingRequired = 3,
                ParticipantsMaxCount = 200,
                MinAgeRequired = 28,
                Description = "Edit!!!!",
            };

            await service.EditAsync(model, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            var editedModel = await service.GetEventViewModelByIdAsync(1);

            Assert.IsNotNull(editedModel);
            Assert.AreEqual("Хапване в Хепи EDIT", editedModel.Name);
            Assert.AreEqual("11.01.2028 00:00", editedModel.StartDate.ToString());
            Assert.IsTrue(editedModel.IsPublic);
            Assert.IsTrue(editedModel.IsVisible);
            Assert.AreEqual(3, editedModel.MinRatingRequired);
            Assert.AreEqual(200, editedModel.ParticipantsMaxCount);
            Assert.AreEqual(28, editedModel.MinAgeRequired);
            Assert.AreEqual("Edit!!!!", editedModel.Description);
        }

        [Test]

        public async Task Test_EditAsync_ShouldThrowExceptionWhenUserIsNotTheOwnerOfTheEvent()
        {
            IEnumerable<int> selectedTypes = new List<int>() { 1 };
            string exmsg = string.Empty;

            EventFormModel model = new EventFormModel()
            {
                Id = 1,
                Name = "Хапване в Хепи EDIT",
                SelectedTypes = selectedTypes,
                StartDate = DateTime.Parse("2028-01-11", CultureInfo.InvariantCulture),
                SelectedPlaceId = 2,
                IsPublic = true,
                IsVisible = true,
                MinRatingRequired = 3,
                ParticipantsMaxCount = 200,
                MinAgeRequired = 28,
                Description = "Edit!!!!",
            };

            try
            {
                await service.EditAsync(model, "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            }
            catch (Exception ex)
            {
                exmsg = ex.Message;
                
            }

            Assert.AreEqual("Access denied", exmsg);

        }

        [Test]
        
        public async Task Test_editAsync_ShouldThrowExceptionWhenTheresNoValidSelectedTypes()
        {
            IEnumerable<int> selectedTypes = new List<int>() { 101 };
            string exmsg = string.Empty;


            EventFormModel model = new EventFormModel()
            {
                Id = 1,
                Name = "Хапване в Хепи EDIT",
                SelectedTypes = selectedTypes,
                StartDate = DateTime.Parse("2028-01-11", CultureInfo.InvariantCulture),
                SelectedPlaceId = 2,
                IsPublic = true,
                IsVisible = true,
                MinRatingRequired = 3,
                ParticipantsMaxCount = 200,
                MinAgeRequired = 28,
                Description = "Edit!!!!",
            };

            try
            {
                await service.EditAsync(model, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            }
            catch (Exception ex)
            {
                exmsg = ex.Message;
            }

            Assert.AreEqual("The eventTypes are not found", exmsg);

        }


    }
}


