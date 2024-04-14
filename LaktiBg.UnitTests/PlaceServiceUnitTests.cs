using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Contracts.PlaceServices;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Core.Services.PlaceServices;
using LaktiBg.Infrastructure.Data;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace LaktiBg.UnitTests
{
    [TestFixture]
    public class PlaceServiceUnitTests
    {
        private ApplicationDbContext dbContext;
        private IEnumerable<Place> places;

        private IRepository repository;
        private IPlaceService placeService;
        private IImageService imageService;

        //Places
        private Place Happy;
        private Place MiroHouse;
        private Place VilaPetra;
        private Place KinoArena;
        private Place CinemaCity;



        [SetUp]

        public async Task Setup()
        {
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

            //Database

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LaktiBgInMemoryDb")
                .Options;

            dbContext = new ApplicationDbContext(options);

            await dbContext.AddRangeAsync(places);
            await dbContext.SaveChangesAsync();


            repository = new Repository(dbContext);
            placeService = new PlaceService(repository, imageService);

        }

        [TearDown]

        public async Task Teardown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]

        public async Task Test_AllAsync_BySearchTerm()
        {
            var result = await placeService.AllAsync(
                "2ed5c668-610f-41a9-8378-f648200ee2a1",
                "Vila Petra",
                1,5);

            Assert.AreEqual(1, result.Places.Count());
            Assert.IsTrue(result.Places.Any(p => p.Id == 3));
            Assert.IsTrue(result.Places.Any(p => p.Name == "Vila Petra"));
        }

        [Test]

        public async Task Test_AllAsync_WithoutSearchTerm()
        {
            var result = await placeService.AllAsync(
            "71368c9b-91fa-4338-bfce-e0921b5324ef",
            null,
            1, 5);

            Assert.AreEqual(3, result.Places.Count());
           
        }

        [Test]

        public async Task Test_AllAsync_ShowUsersPrivatePlace()
        {
            var result = await placeService.AllAsync(
                "2ed5c668-610f-41a9-8378-f648200ee2a1",
                "Kino Arena",
                1, 5);

            Assert.AreEqual(1, result.Places.Count());

        }

        [Test]

        public async Task Test_AllAsync_DontShowPrivatePlaceToOtherUsers()
        {
            var result = await placeService.AllAsync(
              "71368c9b-91fa-4338-bfce-e0921b5324ef",
              "Kino Arena",
              1, 5);

            Assert.AreEqual(0, result.Places.Count());

        }

        [Test]

        public async Task Test_Approve_MethodWorkingCorrectly()
        {
            Place LovenDom = new Place()
            {
                Id = 6,
                Name = "Loven dom",
                OwnerId = "2ed5c668-610f-41a9-8378-f648200ee2a1",
                Contact = "123456789",
                Address = "ул.„Перущица“ 8",
                IsPublic = true,
                Rating = 4.00M,
                IsApproved = false,
            };

            await repository.AddAsync(LovenDom);
            await repository.SaveChangesAsync();

            var result = await placeService.AllAsync(
            "71368c9b-91fa-4338-bfce-e0921b5324ef",
            null,
            1, 5);

            Assert.AreEqual(3, result.Places.Count());


            await placeService.Approve(6);
            await repository.SaveChangesAsync();

            var result2 = await placeService.AllAsync(
               "71368c9b-91fa-4338-bfce-e0921b5324ef",
               null,
               1, 5);

            Place place = result2
                .Places
                .Select(x => new Place
                {
                    Id = x.Id,
                    Name = x.Name,
                    OwnerId = x.OwnerId,
                    Contact = x.Contact,
                    Address = x.Address,
                    IsPublic = x.IsPublic,
                    Rating = x.Rating,
                    IsApproved = x.IsApproved,
                })
                .First(x => x.Id == 6);

            Assert.AreEqual(4, result2.Places.Count());
            Assert.AreEqual(4, result2.TotalPlacesCount);
            Assert.IsTrue(place.IsApproved);
        }

        [Test]

        public async Task Test_Approve_ThrowMsgWhenPlaceIsNotFound()
        {

            try
            {
                await placeService.Approve(6);
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("The place is not found", ex.Message);
               
            }

        }

        [Test]

        public async Task Test_Create_WorkingCorrectly()
        {
            PlaceFormModel model = new PlaceFormModel() 
            {
                Name = "TestPlace",
                Contact = "12344321",
                Address = "Hr.Botev 20",
                IsPublic= true,
                
            };

            await placeService.CreateAsync(model, "71368c9b-91fa-4338-bfce-e0921b5324ef");

            Place? result = await repository.AllReadOnly<Place>()
                                    .Where(x => x.Name == "TestPlace")
                                    .FirstOrDefaultAsync();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsPublic);
            Assert.AreEqual("TestPlace", result.Name);
            Assert.AreEqual("12344321", result.Contact);
            Assert.AreEqual("Hr.Botev 20", result.Address);
            Assert.AreEqual(5, result.Rating);
            Assert.AreEqual("71368c9b-91fa-4338-bfce-e0921b5324ef", result.OwnerId);
            
        }

        [Test]

        public async Task Test_Delete_WorkingCorrectly()
        {
            PlaceFormModel model = new PlaceFormModel()
            {
                Name = "TestPlace",
                Contact = "12344321",
                Address = "Hr.Botev 20",
                IsPublic = true,

            };

            await placeService.CreateAsync(model, "71368c9b-91fa-4338-bfce-e0921b5324ef");

            Place? result = await repository.AllReadOnly<Place>()
                                    .Where(x => x.Name == "TestPlace")
                                    .FirstOrDefaultAsync();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsPublic);
            Assert.AreEqual("TestPlace", result.Name);
            Assert.AreEqual("12344321", result.Contact);
            Assert.AreEqual("Hr.Botev 20", result.Address);
            Assert.AreEqual(5, result.Rating);
            Assert.AreEqual("71368c9b-91fa-4338-bfce-e0921b5324ef", result.OwnerId);

            await placeService.DeletePlace(result.Id);

            Place? result2 = await repository.AllReadOnly<Place>()
                              .Where(x => x.Name == "TestPlace")
                              .FirstOrDefaultAsync();

            Assert.IsNull(result2);

        }

        [Test]

        public async Task Test_Details_WorkingCorrectly()
        {
            var model = await placeService.Details(1);

            Assert.IsNotNull(model);
            Assert.IsTrue(model.IsPublic);
            Assert.AreEqual("Happy Bar & Grill", model.Name);
            Assert.AreEqual("539e62e9-7926-446b-8d9c-92cd370dfde8", model.OwnerId);
            Assert.AreEqual("0700 20 888", model.Contact);
            Assert.AreEqual("ул. „Златю Бояджиев“ 2, 4000 Пловдив", model.Address);
            Assert.AreEqual(5, model.Rating);
            Assert.IsTrue(model.IsApproved);
            Assert.AreEqual(1, model.Id);
        }

        [Test]
        public async Task Test_Details_ThrowExceptionWithNonExistendId()
        {
            try
            {
                var model = await placeService.Details(101);

            }
            catch (Exception ex)
            {
                Assert.AreEqual("The place is not found", ex.Message);
            }
        }

        [Test]

        public async Task Test_Edit_EditShouldWorkCorrectly()
        {
            PlaceFormModel model = new PlaceFormModel()
            {
                Name = "TestPlace",
                Contact = "12344321",
                Address = "Hr.Botev 20",
                IsPublic = true,

            };

            await placeService.CreateAsync(model, "71368c9b-91fa-4338-bfce-e0921b5324ef");

            Place? result = await repository.AllReadOnly<Place>()
                                    .Where(x => x.Name == "TestPlace")
                                    .FirstOrDefaultAsync();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsPublic);
            Assert.AreEqual("TestPlace", result.Name);
            Assert.AreEqual("12344321", result.Contact);
            Assert.AreEqual("Hr.Botev 20", result.Address);
            Assert.AreEqual(5, result.Rating);
            Assert.AreEqual("71368c9b-91fa-4338-bfce-e0921b5324ef", result.OwnerId);

            PlaceFormModel newModel = new PlaceFormModel()
            {
                Id = result.Id,
                Name = "TestPlaceEdit",
                Contact = "12344321987",
                Address = "Hr.Botev EDIT",
                IsPublic = false,

            };

            await placeService.Edit(newModel, "71368c9b-91fa-4338-bfce-e0921b5324ef");

            Place? editedResult = await repository.AllReadOnly<Place>()
                        .Where(x => x.Id == result.Id)
                        .FirstOrDefaultAsync();

            Assert.IsNotNull(editedResult);
            Assert.AreEqual("TestPlaceEdit", editedResult.Name);
            Assert.AreEqual("12344321987", editedResult.Contact);
            Assert.AreEqual("Hr.Botev EDIT", editedResult.Address);
            Assert.IsFalse(editedResult.IsPublic);
        }

        [Test]

        public async Task Test_GetAllUnaprovedAsync_ReturnsTheRightAmount()
        {
            var result = await placeService.GetAllUnaprovedAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test]

        public async Task Test_GetPlaceFormModelByPlaceid_WorkingCorrectly()
        {
            var model = await placeService.GetPlaceFormModelByPlaceId(1);

            Assert.IsNotNull(model);
            Assert.IsTrue(model.IsPublic);
            Assert.AreEqual("Happy Bar & Grill", model.Name);
            Assert.AreEqual("0700 20 888", model.Contact);
            Assert.AreEqual("ул. „Златю Бояджиев“ 2, 4000 Пловдив", model.Address);
            Assert.AreEqual(1, model.Id);
        }

        [Test]

        public async Task Test_IsUserOwner_WorkingCorrectly()
        {
            bool result = await placeService.IsUserOwner("539e62e9-7926-446b-8d9c-92cd370dfde8", 1);

            Assert.IsTrue(result);
        }


        [Test]

        public async Task Test_IsUserOwner_ShouldReturnFalse()
        {
            bool result = await placeService.IsUserOwner("539e62e9-7926-446b-8d9c-92cd370dfde8", 2);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task Test_PlaceExistById_ReturnsTrueWhenUsedWithExistingId()
        {
            bool result = await placeService.PlaceExistById(1);

            Assert.IsTrue(result);
        }

        [Test]

        public async Task Test_PlaceExistById_ReturnsFalseWhenUsedWithNonExistingId()
        {
            bool result = await placeService.PlaceExistById(100001);
            Assert.IsFalse(result);
        }

    }
}
