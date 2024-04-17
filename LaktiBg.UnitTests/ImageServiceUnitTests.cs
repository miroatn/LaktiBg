using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Models.UserModels;
using LaktiBg.Core.Services.ImageServices;
using LaktiBg.Infrastructure.Data;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Globalization;

namespace LaktiBg.UnitTests
{
    [TestFixture]
    public class ImageServiceUnitTests
    {
        private ApplicationDbContext dbContext;

        private IRepository repository;
        private IImageService service;

        private ApplicationUser FirstUser;
        private ApplicationUser SecondUser;
        private Event FirstEvent;
        private Place Happy;

        string imagePath = @"..\..\..\..\LaktiBg\wwwroot\Images\UnitTests\test.jpg";
        private string compressedImagePath = @"..\..\..\..\LaktiBg\wwwroot\Images\UnitTests\CompressedTest.jpg";


        [SetUp]

        public async Task Setup()
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

            //Places

            Happy = new Place()
            {
                Id = 1,
                Name = "Happy Bar & Grill",
                OwnerId = "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386",
                Contact = "0700 20 888",
                Address = "ул. „Златю Бояджиев“ 2, 4000 Пловдив",
                IsPublic = true,
                Rating = 5.00M,
                IsApproved = true,
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


            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LaktiBgInMemoryDb")
                .Options;

            dbContext = new ApplicationDbContext(options);

            await dbContext.AddAsync(FirstUser);
            await dbContext.AddAsync(Happy);
            await dbContext.AddAsync(FirstEvent);
            await dbContext.AddAsync(SecondUser);
            await dbContext.SaveChangesAsync();

            var mockEventService = new Mock<IEventService>();

            mockEventService.Setup(x => x.GetEventByIdAsync(42))
                            .Returns(Task.FromResult(FirstEvent));

            repository = new Repository(dbContext);
            service = new ImageService(repository, mockEventService.Object);
        }

        [TearDown]

        public async Task Teardown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]

        public async Task Test_CompressAndSaveImageAsync_ShouldCompressImageByAGivenQuality()
        {

            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

            await service.CompressAndSaveImageAsync(file, compressedImagePath, 30);
            stream.Close();

            using var stream2 = new FileStream(compressedImagePath, FileMode.Open);

            IFormFile file2 = new FormFile(stream2, 0, stream2.Length, null, Path.GetFileName(stream2.Name));
            stream2.Close();

            Assert.IsTrue(file.Length > file2.Length);


            System.IO.File.Delete(compressedImagePath);

        }

        [Test]

        public async Task Test_GetImagesFromViewModelAsync_ShouldConvertIFormFileCollectionToImage()
        {
            
            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            IFormFileCollection collection = new FormFileCollection() {file};

            var result = await service.GetImagesFromViewModelAsync(collection, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            stream.Close();

            Image image = result.First();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Image", image.GetType().Name);

        }

        [Test]

        public async Task Test_GetImagesByIdAsync_ShouldReturnImagesFromGivenPlaceId()
        {
            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            IFormFileCollection collection = new FormFileCollection() { file };

            var result = await service.GetImagesFromViewModelAsync(collection, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            stream.Close();

            Image image = result.First();

            Happy.Images.Add(image);
            await repository.AddAsync(image);
            await repository.SaveChangesAsync();

            var images = await service.GetImagesByIdAsync(1, "Place");

            Assert.AreEqual(1, images.Count);

        }

        [Test]

        public async Task Test_GetImagesByIdAsync_ShouldReturnImagesFromGivenEventId()
        {
            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            IFormFileCollection collection = new FormFileCollection() { file };

            var result = await service.GetImagesFromViewModelAsync(collection, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            stream.Close();

            Image image = result.First();

            FirstEvent.Images.Add(image);
            await repository.AddAsync(image);
            await repository.SaveChangesAsync();

            var images = await service.GetImagesByIdAsync(42, "Event");

            Assert.AreEqual(1, images.Count);

        }

        [Test]

        public async Task Test_ConvertImagesToStringAsync_ShouldConvertImagesToString()
        {
            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            IFormFileCollection collection = new FormFileCollection() { file };

            var result = await service.GetImagesFromViewModelAsync(collection, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            stream.Close();

            Image image = result.First();
            List<Image> images = new List<Image>() {image};

            Dictionary<int,string> stringImages = await service.ConvertImagesToStringAsync(images);
            string imageToString = stringImages.Values.First();

            Assert.IsNotNull(stringImages);

            string expectedResult = "data:image/png;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/wAARCAFDAa4DASIAAhEBAxEB/8QBogAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoLEAACAQMDAgQDBQUEBAAAAX0BAgMABBEFEiExQQYTUWEHInEUMoGRoQgjQrHBFVLR8CQzYnKCCQoWFxgZGiUmJygpKjQ1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4eLj5OXm5+jp6vHy8/T19vf4+foBAAMBAQEBAQEBAQEAAAAAAAABAgMEBQYHCAkKCxEAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9sAhAAbEhQXFBEbFxYXHhwbIChCKyglJShROj0wQmBVZWRfVV1baniZgWpxkHNbXYW1hpCeo6utq2eAvMm6pseZqKukARweHigjKE4rK06kbl1upKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKT/2gAMAwEAAhEDEQA/ANqiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooAWiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAFopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaY8scf33A+poAdRUXn5+5HI3vtwP1ppmcfwIv8AvSf4UAT0VW+0P/fgH4n/AApRNIejwH/gRouBYoqISTf88lb/AHXH9aPPC/6yORPcrkfmKAJaKRJEkGUYN9DTqAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKACilooASilooASilooASilooASilooASilpruEx3J6AdTQAtRGbORGu8jv0A/GoZph/Gd3+yDwPqe9Q7p7nhOEHfoBSAmlmUf62Yn/Yj4H51CLgk4t4QvvjJqWKyjXl/nPv0qyAFGAAB7UWGUvJuZeXcj6mlFiP4nz+FXKKdhFUWMfqfyoNjH6n8qtUUAUzY4+5Jik2XkX3HLD65q7RQBnm5+bE8I3eoG01Yinz/q5N3+zJwfzqdlVxhgCPQ1VlsV6xHafQ9KQFtJVc7SCr/3W60+swSPGRHMpIHQE9PoauRT/LktuTpu7j2NAE9FLRTASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooAWiiigAooooAKKKKACiiigAoopkj7RheWPQUAJJJt+VcbvfoPrVKSUtkITg9W7t9PQUsjb8qDlc8n++f8Knhh2fM3LfypDIorXOGl/BatAYGBwKWimISiloxQAlFLilxQA2inYoxQA2inYoxQA2inYoxQBHJGsi7XGRVNke2fIOVPGT0I9DV/FIyBlKsMg0ARQyAAEZ8snGD1U+lWaoAGCQq3KkYPuKtQsRlCc46H1FLYZLRRRTEFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUALRS0UAJRS0UAQz3MNsFM0gTd0z3qMajZH/l5j/OsrxH/wAfEP8AuH+dZFZubTOqnQUoptnWi+tD0uYv++qcLu2PSeL/AL7FchRRzlfVl3OyE0RHyyI3sGBqtKxPGcM45PotZ+iWwCGdhy3APoO9Wp5CsbSdGbp7elUndXOeUbS5UV31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AJtpQKXFLQAgFLijFLQAUtGKKAFpaSloAdTTRSUABppp1JQAw00ipMUmKAIitNKVNikxQBAUpPLqfFGKAIPLo2VNikxQBFso21LijFAEe2jFSYoxQBHilxT8UmKAG4o207FGKAG7aNtOxRigBu2jbTsUUANxS4paKAExRilooATFGKWigAxRiiigAxS4opaAExS4opaADFGKWloATFGKWloAbijFOpaAG4oxTqWgBuKMU7FLigBuKXFOxRigBMUYp2KXFADcUuKXFLigBuKXFLiigBlJRSUAFFFJQAUlFFABSUUUAJRRRQAlFGKMUAJRS4oxQAlFLijFACUUuKMUAJSU7FGKAExRilxS4oAbijFOxRigBuKMU/FGKAGYoxT8Uu2gCPFG2pNtG2gCPFGKk20baAI8UuKfto20AMxS4p+2jFADKWnYoxQA2lpcUYoASloxRigApaSigBaWkooAdRSUUAOpabS5oAdRSUuaAFopKWgCKinYpMUANxRinYoxQAzFGKdijFADMUYp+KMUAMxRin4oxQAzFGKfijFADMUYp+KMUAMxRin4oxQAzFGKfijFADMUYp+KMUAMxRin4oxQA3FGKfijFADcUYp2KMUANxS4p2KMUANxS4pcUuKAG4oxTqKAG4oxTqKAG4oxTqMUANxRinYoxQA3FGKdijFADMUYp+KTFADcUYp2KMUANxRinYoxQA2lpcUYoASilooAKWiigApaSloATFGKWigBMUYpaKAExRilxRigBMUYpcUtADcUYp1FADcUYp1FADcUYp1FADcUYpaKAExRilooATFGKWigBMUYpaKAExRilooATFGKWigBKKWigAooooAKKKKACilooASilooASilooASilooASiiigApKWigBKKWigBKKWigBKKWigBKWiigAoopaACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACkpaSgAooooAKKKKACiiigAooooAKWkpaACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooASilooAKSlooAKKKKACiiigD//Z";

            Assert.AreEqual(expectedResult, imageToString);


        }

        [Test]

        public async Task Test_ConvertImageToStringAsync_ShouldConvertImageToString()
        {
            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            IFormFileCollection collection = new FormFileCollection() { file };

            var result = await service.GetImagesFromViewModelAsync(collection, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            stream.Close();

            Image image = result.First();

            string imageToString = await service.ConvertImageToStringAsync(image);

            Assert.IsNotNull(imageToString);

            string expectedResult = "data:image/png;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/wAARCAFDAa4DASIAAhEBAxEB/8QBogAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoLEAACAQMDAgQDBQUEBAAAAX0BAgMABBEFEiExQQYTUWEHInEUMoGRoQgjQrHBFVLR8CQzYnKCCQoWFxgZGiUmJygpKjQ1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4eLj5OXm5+jp6vHy8/T19vf4+foBAAMBAQEBAQEBAQEAAAAAAAABAgMEBQYHCAkKCxEAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9sAhAAbEhQXFBEbFxYXHhwbIChCKyglJShROj0wQmBVZWRfVV1baniZgWpxkHNbXYW1hpCeo6utq2eAvMm6pseZqKukARweHigjKE4rK06kbl1upKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKT/2gAMAwEAAhEDEQA/ANqiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooAWiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAFopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaY8scf33A+poAdRUXn5+5HI3vtwP1ppmcfwIv8AvSf4UAT0VW+0P/fgH4n/AApRNIejwH/gRouBYoqISTf88lb/AHXH9aPPC/6yORPcrkfmKAJaKRJEkGUYN9DTqAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKAEopaKACilooASilooASilooASilooASilooASilpruEx3J6AdTQAtRGbORGu8jv0A/GoZph/Gd3+yDwPqe9Q7p7nhOEHfoBSAmlmUf62Yn/Yj4H51CLgk4t4QvvjJqWKyjXl/nPv0qyAFGAAB7UWGUvJuZeXcj6mlFiP4nz+FXKKdhFUWMfqfyoNjH6n8qtUUAUzY4+5Jik2XkX3HLD65q7RQBnm5+bE8I3eoG01Yinz/q5N3+zJwfzqdlVxhgCPQ1VlsV6xHafQ9KQFtJVc7SCr/3W60+swSPGRHMpIHQE9PoauRT/LktuTpu7j2NAE9FLRTASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooASilooAWiiigAooooAKKKKACiiigAoopkj7RheWPQUAJJJt+VcbvfoPrVKSUtkITg9W7t9PQUsjb8qDlc8n++f8Knhh2fM3LfypDIorXOGl/BatAYGBwKWimISiloxQAlFLilxQA2inYoxQA2inYoxQA2inYoxQBHJGsi7XGRVNke2fIOVPGT0I9DV/FIyBlKsMg0ARQyAAEZ8snGD1U+lWaoAGCQq3KkYPuKtQsRlCc46H1FLYZLRRRTEFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUAFFFFABRRRQAUUUUALRS0UAJRS0UAQz3MNsFM0gTd0z3qMajZH/l5j/OsrxH/wAfEP8AuH+dZFZubTOqnQUoptnWi+tD0uYv++qcLu2PSeL/AL7FchRRzlfVl3OyE0RHyyI3sGBqtKxPGcM45PotZ+iWwCGdhy3APoO9Wp5CsbSdGbp7elUndXOeUbS5UV31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AJtpQKXFLQAgFLijFLQAUtGKKAFpaSloAdTTRSUABppp1JQAw00ipMUmKAIitNKVNikxQBAUpPLqfFGKAIPLo2VNikxQBFso21LijFAEe2jFSYoxQBHilxT8UmKAG4o207FGKAG7aNtOxRigBu2jbTsUUANxS4paKAExRilooATFGKWigAxRiiigAxS4opaAExS4opaADFGKWloATFGKWloAbijFOpaAG4oxTqWgBuKMU7FLigBuKXFOxRigBMUYp2KXFADcUuKXFLigBuKXFLiigBlJRSUAFFFJQAUlFFABSUUUAJRRRQAlFGKMUAJRS4oxQAlFLijFACUUuKMUAJSU7FGKAExRilxS4oAbijFOxRigBuKMU/FGKAGYoxT8Uu2gCPFG2pNtG2gCPFGKk20baAI8UuKfto20AMxS4p+2jFADKWnYoxQA2lpcUYoASloxRigApaSigBaWkooAdRSUUAOpabS5oAdRSUuaAFopKWgCKinYpMUANxRinYoxQAzFGKdijFADMUYp+KMUAMxRin4oxQAzFGKfijFADMUYp+KMUAMxRin4oxQAzFGKfijFADMUYp+KMUAMxRin4oxQA3FGKfijFADcUYp2KMUANxS4p2KMUANxS4pcUuKAG4oxTqKAG4oxTqKAG4oxTqMUANxRinYoxQA3FGKdijFADMUYp+KTFADcUYp2KMUANxRinYoxQA2lpcUYoASilooAKWiigApaSloATFGKWigBMUYpaKAExRilxRigBMUYpcUtADcUYp1FADcUYp1FADcUYp1FADcUYpaKAExRilooATFGKWigBMUYpaKAExRilooATFGKWigBKKWigAooooAKKKKACilooASilooASilooASilooASiiigApKWigBKKWigBKKWigBKKWigBKWiigAoopaACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACkpaSgAooooAKKKKACiiigAooooAKWkpaACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooASilooAKSlooAKKKKACiiigD//Z";

            Assert.AreEqual(expectedResult, imageToString);
        }

        [Test]

        public async Task Test_DeleteImageShouldDeleteImageByGivenId()
        {
            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            IFormFileCollection collection = new FormFileCollection() { file };

            var result = await service.GetImagesFromViewModelAsync(collection, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            stream.Close();

            Image image = result.First();

            await repository.AddAsync(image);
            await repository.SaveChangesAsync();

            var imageFromDB = await repository.GetByIdAsync<Image>(image.Id);

            Assert.IsNotNull(imageFromDB);

            await service.DeleteImage(image.Id);

            var imageFromDBEdit = await repository.GetByIdAsync<Image>(image.Id);

            Assert.IsNull(imageFromDBEdit);
        }

        [Test]

        public async Task Test_SaveImagesToEventAsync_ShouldSaveImageToAnEvent()
        {
            var imagesBeforeMethod = FirstEvent.Images;

            Assert.AreEqual(0, imagesBeforeMethod.Count);

            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            IFormFileCollection collection = new FormFileCollection() { file };
            await service.SaveImagesToEventAsync(collection, 42, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            stream.Close();

            var images = FirstEvent.Images;

            Assert.AreEqual(1, images.Count);
        }

        [Test]

        public async Task Test_ConvertFileToImageAsync_ShouldConvertIFormFileToImage()
        {
            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

            var result = await service.ConvertFileToImageAsync(file);

            stream.Close();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.GetType().Name, Is.EqualTo("Image"));

        }

        [Test]

        public async Task Test_ConvertFileToImageAsync_ShouldReturnNullIfTheresNoFile()
        {
            IFormFile file = null;

            var result = await service.ConvertFileToImageAsync(file);

            Assert.That(result, Is.Null);
        }

        [Test]

        public async Task Test_AddNewUserAvatar_ShouldAddNewAvatarToUser()
        {
            var userAvatar = FirstUser.Avatar;

            Assert.IsNull(userAvatar);

            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
           
            UserViewModel model = new UserViewModel() 
            {
                File = file,
            };

            await service.AddNewUserAvatar(model, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            stream.Close();

            var userUpdatedAvatar = FirstUser.Avatar;

            Assert.IsNotNull(userUpdatedAvatar);
        }

        [Test]

        public async Task Test_CheckIfUserIsTheImageAuthor_ShouldReturnTrueIfUserIsTheAuthor()
        {
            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            IFormFileCollection collection = new FormFileCollection() { file };
            await service.SaveImagesToEventAsync(collection, 42, "bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386");

            stream.Close();

            var result = await service.CheckIfUserIsTheImageAuthor("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386", 1);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Test_CheckIfUserIsTheImageAuthor_ShouldReturnFalseIfUserIsNotTheAuthor()
        {
            using var stream = new FileStream(imagePath, FileMode.Open);

            IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            IFormFileCollection collection = new FormFileCollection() { file };
            await service.SaveImagesToEventAsync(collection, 42, "fb4143db-57af-4ebe-950c-b17e1b3c4fb4");

            stream.Close();

            var result = await service.CheckIfUserIsTheImageAuthor("bf6468c5-f4eb-46a2-8fd7-1ab0d5f8d386", 1);

            Assert.IsFalse(result);
        }
    }
}
