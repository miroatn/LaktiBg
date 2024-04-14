using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Models.UserModels;
using LaktiBg.Core.Services.UserServices;
using LaktiBg.Infrastructure.Data;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Numerics;

namespace LaktiBg.UnitTests
{
    [TestFixture]
    public class UserServiceUnitTests
    {
        private ApplicationDbContext dbContext;
        private IEnumerable<ApplicationUser> users;
        private IEnumerable<Place> places;
        private IEnumerable<Event> events;
        private IEnumerable<UsersEvents> usersEvents;


        private IRepository repository;
        private IUserService service;

        //Users
        private ApplicationUser FirstUser;
        private ApplicationUser SecondUser;
        private ApplicationUser ThirdUser;

        //Events
        private Event FirstEvent;
        private Event SecondEvent;
        private Event ThirdEvent;

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

        [SetUp]

        public async Task SetUp() 
        {
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
                Id = 42,
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
                Id = 43,
                Name = "На домашно у Миро",
                CreationDate = DateTime.Parse("2024-03-12", CultureInfo.InvariantCulture),
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
                Id = 44,
                Name = "Дюн 2",
                CreationDate = DateTime.Parse("2024-03-14", CultureInfo.InvariantCulture),
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

            events = new List<Event>()
            {
                FirstEvent, SecondEvent, ThirdEvent
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


            //Database

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LaktiBgInMemoryDb")
                .Options;

            dbContext = new ApplicationDbContext(options);

            await dbContext.AddRangeAsync(users);
            await dbContext.AddRangeAsync(places);
            await dbContext.AddRangeAsync(events);
            await dbContext.AddRangeAsync(usersEvents);
            await dbContext.SaveChangesAsync();

            repository = new Repository(dbContext);
            service = new UserService(repository);
        }

        [TearDown]

        public async Task Teardown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]

        public async Task Test_ExistById_ReturnsTrueWhenUserIsInBase()
        {
            bool result = await service.ExistById("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsTrue(result);
        }

        [Test]

        public async Task Test_ExistById_ReturnsFalseIfUserIsNotFound()
        {
            bool result = await service.ExistById("f7fa553c-27ed-48ad-8768-d08451064d79");

            Assert.IsFalse(result);
        }

        [Test]

        public async Task Test_GetUsersNameByIdAsync_ReturnsUsersFullNameWhenUserIsNotNull()
        {
            string result = await service.GetUsersNameByIdAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsNotNull(result);
            Assert.AreEqual("Petur Nikolov", result);
        }

        [Test]

        public async Task Test_GetUsersAgeById_ReturnsUsersAgeWhenUserIsNotNull()
        {
            int result = await service.GetUsersAgeById("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsNotNull(result);
            Assert.AreEqual(31, result);
        }

        [Test]

        public async Task Test_GetUsersAgeById_ThrowsExceptionIfUserIsNotInDatabase()
        {
            try
            {
                int result = await service.GetUsersAgeById("d9d6b4a5-f3f2-47bd-ad2b-f362632f83be");

            }
            catch (Exception ex)
            {
                Assert.AreEqual("You dont have permitions for this action", ex.Message);
            }
        }

        [Test]

        public async Task Test_GetUsersRatingById_ReturnsUsersRating()
        {
            decimal result = await service.GetUsersRatingById("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");
            decimal result2 = await service.GetUsersRatingById("fb4143db-57af-4ebe-950c-b17e1b3c4fb4");
            decimal result3 = await service.GetUsersRatingById("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            Assert.AreEqual(3, result);
            Assert.AreEqual(5, result2);
            Assert.AreEqual(5, result3);

        }

        [Test]

        public async Task Test_GetUserViewModelByIdAsync_ReturnsUserViewModelWhenUserIsInDB()
        {
            UserViewModel result = await service.GetUserViewModelByIdAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            Assert.IsNotNull(result);
            Assert.AreEqual("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386", result.Id);
            Assert.AreEqual("Ivan", result.FirstName);
            Assert.AreEqual("Draganov", result.LastName);
            Assert.AreEqual("11.4.2010 г. 0:00:00", result.BirthDate.ToString());
            Assert.AreEqual(5, result.Rating);
            Assert.AreEqual(0, result.Friends.Count);
            Assert.AreEqual("Ulica Sezam 42", result.Address);
            Assert.AreEqual("Ivans description", result.Description);
            Assert.AreEqual("ivandraganov@abv.bg", result.UserName);
            Assert.AreEqual("1234567890", result.PhoneNumber);
            Assert.IsNull(result.AvatarBytes);
            Assert.IsEmpty(result.FinishedEvents);
            Assert.IsEmpty(result.OngoingEvents);
        }

        [Test]

        public async Task Test_GetUsersFinishedEventsAsync_ReturnsOnlyFinishedEventsOfTheUser()
        {
            var result = await service.GetUsersFinishedEventsAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4");
            var result2 = await service.GetUsersFinishedEventsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");


            Assert.IsNotNull(result);
            Assert.IsNotNull(result2);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result2.Count);
        }

        [Test]

        public async Task Test_GetUsersAllEventsAsync_ReturnsAllUsersEvents()
        {
            var result = await service.GetUsersAllEventsAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                                                                1, 10);
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.TotalEventsCount);
            Assert.AreEqual(3, result.Events.Count());
        }

        [Test]

        public async Task Test_GetUsersOnGoingEventAsync_ReturnsOnlyEventsThatAreNotFinished()
        {
            var result = await service.GetUsersOnGoingEventsAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4");
            var result2 = await service.GetUsersOnGoingEventsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsNotNull(result);
            Assert.IsEmpty(result2);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0, result2.Count);

        }

        [Test]
        public async Task Test_GetuserEventsAsync_ReturnsAllEventsThatUserIsOrganizerOf()
        {
            var result = await service.GetUserEventsAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386", 1,10);
            var result2 = await service.GetUserEventsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be", 1, 10);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result2);
            Assert.AreEqual(4, result.TotalEventsCount);
            Assert.AreEqual(0, result2.TotalEventsCount);
            Assert.AreEqual(4, result.Events.Count());
            Assert.AreEqual(0, result2.Events.Count());

        }

        [Test]

        public async Task Test_AddFriendAsync_AddingFriendWhenUserIdsAreRegisteredInDatabase()
        {
            var userUserFriends = await service.GetUserFriendsAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            var secondUserFriends = await service.GetUserFriendsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");


            Assert.AreEqual(0, userUserFriends.Count);
            Assert.AreEqual(0, secondUserFriends.Count);

            await service.AddFriendAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            await service.AcceptFriendRequestAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                                                    "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            var userFriendsUpdated = await service.GetUserFriendsAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            var userFriendFriends = await service.GetUserFriendsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.AreEqual(1, userFriendsUpdated.Count);
            Assert.AreEqual(1, userFriendFriends.Count);
        }

        [Test]

        public async Task Test_CheckIfUserIsFriend_ShouldReturnFalseIfTheyAreNotFriends()
        {
            bool result = await service.CheckIfUserIsFriend("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386"
                                                          , "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsFalse(result);
        }

        [Test]

        public async Task Test_CheckIfUserIsFriend_ShouldReturnTrueIfTheyAreFriends()
        {
            var userUserFriends = await service.GetUserFriendsAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            var secondUserFriends = await service.GetUserFriendsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");
            bool result = await service.CheckIfUserIsFriend("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.AreEqual(0, userUserFriends.Count);
            Assert.AreEqual(0, secondUserFriends.Count);
            Assert.IsFalse(result);
            

            await service.AddFriendAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            await service.AcceptFriendRequestAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                                                    "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            var userFriendsUpdated = await service.GetUserFriendsAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            var userFriendFriends = await service.GetUserFriendsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");
            bool updatedResult = await service.CheckIfUserIsFriend("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                            "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.AreEqual(1, userFriendsUpdated.Count);
            Assert.AreEqual(1, userFriendFriends.Count);
            Assert.IsTrue(updatedResult);

        }

        [Test]

        public async Task Test_GetFriendRequestsAsync_ReturnsFriendRequestsIfThereAreAny()
        {
            var userUserFriends = await service.GetUserFriendsAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            var secondUserFriends = await service.GetUserFriendsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");


            Assert.AreEqual(0, userUserFriends.Count);
            Assert.AreEqual(0, secondUserFriends.Count);


            await service.AddFriendAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            var result = await service.GetFriendRequestsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");
            Assert.AreEqual(1, result.Count);
        }

        [Test]

        public async Task Test_GetFriendRequestsAsync_ReturnsEmptyListWhenTheresNoFriendRequest()
        {

            var result = await service.GetFriendRequestsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");
            Assert.AreEqual(0, result.Count);
        }

        [Test]

        public async Task Test_GetFriendRequestCountAsync_ReturnsTheCountOfTheFriendRequests()
        {
            int count = int.Parse(await service.GetFriendRequestCountAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be"));
            Assert.AreEqual(0, count);

            await service.AddFriendAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            int updatedCount = int.Parse(await service.GetFriendRequestCountAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be"));
            Assert.AreEqual(1, updatedCount);
        }

        [Test]

        public async Task Test_RemoveFriendAsync_ShouldRemoveAFriendIfTheresOne()
        {
            var userUserFriends = await service.GetUserFriendsAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            var secondUserFriends = await service.GetUserFriendsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");


            Assert.AreEqual(0, userUserFriends.Count);
            Assert.AreEqual(0, secondUserFriends.Count);

            await service.AddFriendAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            await service.AcceptFriendRequestAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                                                    "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            var userFriendsUpdated = await service.GetUserFriendsAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            var userFriendFriends = await service.GetUserFriendsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.AreEqual(1, userFriendsUpdated.Count);
            Assert.AreEqual(1, userFriendFriends.Count);

            await service.RemoveFriendAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                                                    "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            var userFriendsDeleted = await service.GetUserFriendsAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");
            var userFriendDeleted = await service.GetUserFriendsAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.AreEqual(0, userFriendsDeleted.Count);
            Assert.AreEqual(0, userFriendDeleted.Count);
        }

        [Test]

        public async Task Test_UpdateUserRatingAsync_ShouldUpdateUsersRatingPositive()
        {
            decimal rating = await service.GetUsersRatingById("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.AreEqual(3, rating);

            await service.UpdateUserRatingAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be", "up");
            decimal updatedRating = await service.GetUsersRatingById("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.AreEqual(3.05, updatedRating);

        }

        [Test]

        public async Task Test_UpdateUserRatingAsync_ShouldUpdateUsersRatingNegative()
        {
            decimal rating = await service.GetUsersRatingById("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.AreEqual(3, rating);

            await service.UpdateUserRatingAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be", "down");
            decimal updatedRating = await service.GetUsersRatingById("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.AreEqual(2.95, updatedRating);
        }

        [Test]

        public async Task Test_CheckIfUserCanVoteAsync_ShouldReturnTrueIfUsersHaveNewVisitedEvent()
        {

            await service.AddFriendAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            await service.AcceptFriendRequestAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                                                    "fb4143db-57af-4ebe-950c-b17e1b3c4fb4");

            bool result = await service.CheckIfUserCanVoteAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsTrue(result);
        }

        [Test]

        public async Task Test_CheckIfUserCanVoteAsync_ShouldReturnFalseIfUsersHaveNotVisitedNewEventTogether()
        {

            await service.AddFriendAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            await service.AcceptFriendRequestAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                                                    "fb4143db-57af-4ebe-950c-b17e1b3c4fb4");

            bool result = await service.CheckIfUserCanVoteAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsTrue(result);

            bool updatedResult = await service.CheckIfUserCanVoteAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsFalse(updatedResult);
        }

        [Test]

        public async Task Test_GetFriendsSameEventCounterAsync_ShouldReturnTheCountOfTheSameParticipatedEvents()
        {
            int result = await service.GetFriendsSameEventCounterAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.AreEqual(1, result);
        }

        [Test]

        public async Task Test_EditUserAsync_ShouldEditUserCorrectly()
        {
            UserEditModel model = new UserEditModel() 
            {
                FirstName = "IvanEdit",
                LastName = "DraganovEdit",
                BirthDate = DateTime.Parse("2011-12-12", CultureInfo.InvariantCulture),
                Address = "EditedAddress",
                Description = "EditedDescription",
                PhoneNumber = "111111111",

            };

            await service.EditUserAsync(model, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            UserViewModel editedModel = await service.GetUserViewModelByIdAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            Assert.IsNotNull(editedModel);
            Assert.AreEqual("IvanEdit", editedModel.FirstName);
            Assert.AreEqual("DraganovEdit", editedModel.LastName);
            Assert.AreEqual("12.12.2011 г. 0:00:00", editedModel.BirthDate.ToString());
            Assert.AreEqual("EditedAddress", editedModel.Address);
            Assert.AreEqual("EditedDescription", editedModel.Description);
            Assert.AreEqual("111111111", editedModel.PhoneNumber);
            
        }

        [Test]

        public async Task Test_GetUserEditModelAsync_ShouldReturnTheEditedModel()
        {
            UserEditModel model = new UserEditModel()
            {
                FirstName = "IvanEdit",
                LastName = "DraganovEdit",
                BirthDate = DateTime.Parse("2011-12-12", CultureInfo.InvariantCulture),
                Address = "EditedAddress",
                Description = "EditedDescription",
                PhoneNumber = "111111111",

            };

            await service.EditUserAsync(model, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            UserEditModel editedModel = await service.GetUserEditModelAsync("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            Assert.IsNotNull(editedModel);
            Assert.AreEqual("IvanEdit", editedModel.FirstName);
            Assert.AreEqual("DraganovEdit", editedModel.LastName);
            Assert.AreEqual("12.12.2011 г. 0:00:00", editedModel.BirthDate.ToString());
            Assert.AreEqual("EditedAddress", editedModel.Address);
            Assert.AreEqual("EditedDescription", editedModel.Description);
            Assert.AreEqual("111111111", editedModel.PhoneNumber);
        }

        [Test]

        public async Task Test_CheckIfUsersAreAlreadyFriendsAsync_ShouldReturnFalseIfTheUsersAreNotFriends()
        {
            bool result = await service.CheckIfUsersAreAlreadyFriendsAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsFalse(result);
        }

        [Test]

        public async Task Test_CheckIfUsersAreAlreadyFriendsAsync_ShouldReturnTrueWhenUsersAreFriends()
        {
            await service.AddFriendAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            await service.AcceptFriendRequestAsync("d9d6b4a5-f3f8-47bd-ad2b-f362632f83be",
                                                    "fb4143db-57af-4ebe-950c-b17e1b3c4fb4");

            bool result = await service.CheckIfUsersAreAlreadyFriendsAsync("fb4143db-57af-4ebe-950c-b17e1b3c4fb4",
                                        "d9d6b4a5-f3f8-47bd-ad2b-f362632f83be");

            Assert.IsTrue(result);
        }
    }
}
