using LaktiBg.Core.Contracts;
using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Models.CommentModels;
using LaktiBg.Core.Services.CommentServices;
using LaktiBg.Core.Services.EventServices;
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
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.UnitTests
{
    [TestFixture]
    public class CommentServiceUnitTests
    {
        private ApplicationDbContext dbContext;
        private IEnumerable<ApplicationUser> users;
        private IEnumerable<Place> places;
        private IEnumerable<Event> events;
        private IEnumerable<UsersEvents> usersEvents;
        private IEnumerable<EventType> eventTypes;
        private IEnumerable<EventTypeConnection> eventTypeConnections;
        private IEnumerable<Comment> comments;

        private IRepository repository;
        private ICommentService service;

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

        //Comments

        private Comment FirstComment;
        private Comment SecondComment;
        private Comment ThirdComment;

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
                Id = 2,
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
                EventId = 4,
            };

            SecondUserEvent = new UsersEvents()
            {
                UserId = "fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                EventId = 5,
            };

            ThirdUserEvent = new UsersEvents()
            {
                UserId = "fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                EventId = 6,
            };

            FourthUserEvent = new UsersEvents()
            {
                UserId = "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                EventId = 6,

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

            //Comments 

            FirstComment = new Comment
            {
                Id = 1,
                Text = "This is the first comment",
                AuthorId = "fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                EventId = 6,
                DateTime = DateTime.Parse("2024-04-11 10:10:10", CultureInfo.InvariantCulture)

            };

            SecondComment = new Comment
            {
                Id = 2,
                Text = "This is second comment, but first of  this user",
                AuthorId = "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                EventId = 6,
                DateTime = DateTime.Parse("2024-04-12 20:20:20", CultureInfo.InvariantCulture)

            };

            ThirdComment = new Comment
            {
                Id = 3,
                Text = "This is third comment and second of this user",
                AuthorId = "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                EventId = 6,
                DateTime = DateTime.Parse("2024-04-12 21:21:21", CultureInfo.InvariantCulture)
            };

            comments = new List<Comment>()
            {
                FirstComment,
                SecondComment,
                ThirdComment,
            };

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
            await dbContext.AddRangeAsync(comments);
            await dbContext.SaveChangesAsync();

            var mockUserService = new Mock<IUserService>();

            mockUserService.Setup(x => x.GetUsersNameByIdAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be"))
                           .Returns(Task.FromResult("Petur Nikolov"));

            var mockEventService = new Mock<IEventService>();

            mockEventService.Setup(x => x.GetEventNameByIdAsync(6))
                            .Returns(Task.FromResult("Дюн 5"));

            repository = new Repository(dbContext);
            service = new CommentService(repository, mockUserService.Object, mockEventService.Object);

        }

        [TearDown]

        public async Task Teardown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]

        public async Task Test_AddCommentAsync_ShouldAddNewCommentToSelectedEvent()
        {
            CommentFormModel viewModel = new CommentFormModel()
            {
                Text = "Test adding new comment",
                AuthorId = "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                EventId = 6,

            };


            await service.AddCommentAsync(viewModel, 6);

            var result = await service.GetCommentsByEventIdAsync(6,
                "Test adding new comment",
                1,
                10);

            var comment = result.Comments.First();

            Assert.IsNotNull(comment);
            Assert.AreEqual("Test adding new comment", comment.Text);
            Assert.AreEqual("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be", comment.AuthorId);
            Assert.AreEqual(6, comment.EventId);
            Assert.AreEqual("Petur Nikolov", comment.AuthorName);
            Assert.AreEqual("Дюн 5", comment.EventTitle);
            
        }

        [Test]

        public async Task Test_CommentExistByIdAsync_ShouldReturnTrueIfCommentIdExistInDB()
        {
            bool result = await service.CommentExistByIdAsync(1);

            Assert.IsTrue(result);
        }

        [Test]

        public async Task Test_CommentExistByIdAsync_ShouldReturnFalseIfCommentIdDoesNotExistInDB()
        {
            bool result = await service.CommentExistByIdAsync(1001);

            Assert.IsFalse(result);
        }

        [Test]

        public async Task Test_DeleteAsync_ShouldDeleteCommentByGivenId()
        {
            var result = await service.GetCommentsByEventIdAsync(6,
                            "This is the first comment",
                            1,
                            10);

            Assert.AreEqual(1, result.TotalCommentsCount);

            await service.DeleteAsync(1);

            var editedResult = await service.GetCommentsByEventIdAsync(6,
                "This is the first comment",
                1,
                10);


            Assert.AreEqual(0, editedResult.TotalCommentsCount);
        }

        [Test]

        public async Task Test_GetCommentsByEventIdAsync_ShouldReturnTheRightAmountOfComments()
        {
            var comments = await service.GetCommentsByEventIdAsync(6,null,1,10);

            Assert.AreEqual(3, comments.TotalCommentsCount);
            Assert.AreEqual(3, comments.Comments.Count());
        }

        [Test]

        public async Task Test_GetCommentsByEventIdAsync_ShouldReturnOnlyTheSearchedComment()
        {
            var comments = await service.GetCommentsByEventIdAsync(6, "This is the first comment", 1, 10);

            Assert.AreEqual(1, comments.Comments.Count());
            Assert.AreEqual(1, comments.TotalCommentsCount);
        }

        [Test]

        public async Task Test_IsUserOwnerOfCommentAsync_ReturnsTrueWhenTheUserIsOwnerOfTheComment()
        {
            bool result = await service.IsUserOwnerOfCommentAsync(1, 6, "fb4143db-57af-4ebe-950c-b17e1b3c4fb4");

            Assert.IsTrue(result);
        }

        [Test]

        public async Task Test_IsUserOwnerOfCommentAsync_ReturnsFalseWhenTheUserIsNotTheOwnerOfTheComment()
        {
            bool result = await service.IsUserOwnerOfCommentAsync(1, 6, "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsFalse(result);
        }

    }
}
