using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Models.ImageModels;
using LaktiBg.Core.Models.UserModels;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using static System.Net.Mime.MediaTypeNames;

namespace LaktiBg.Core.Services.ImageServices
{
    public class ImageService : IImageService
    {
        private readonly IRepository repository;
        private readonly IEventService eventService;

        public ImageService(IRepository _repository, IEventService _eventService)
        {
            repository = _repository;
            eventService = _eventService;
        }

        public async Task CompressAndSaveImageAsync(IFormFile imageFile, string outputPath, int quality)
        {
            using var imageStream = imageFile.OpenReadStream();
            using var image = SixLabors.ImageSharp.Image.Load(imageStream);
            var encoder = new JpegEncoder
            {
                Quality = quality, // Adjust this value for desired compression quality
            };

            await Task.Run(() => image.Save(outputPath, encoder));

        }

        public async Task<IList<Infrastructure.Data.Models.Image>> GetImagesFromViewModelAsync(IFormFileCollection files, string userId)
        {
            List<ImageViewModel> imageModels = new List<ImageViewModel>();

            if (files != null)
            {
                foreach (var file in files)
                {
                    using (var memoryStream = new MemoryStream())
                    {

                        var outputPath = Path.Combine("uploads", file.FileName);
                        await CompressAndSaveImageAsync(file, outputPath, 30);

                        string fileExtention = Path.GetExtension(file.FileName.ToLower());
                        byte[] reducedFile = File.ReadAllBytes(outputPath);

                        await file.CopyToAsync(memoryStream);


                        if (reducedFile.Length < 2097152 && isPhoto(fileExtention))
                        {
                            var newphoto = new ImageViewModel()
                            {
                                Bytes = reducedFile,
                                Description = file.FileName,
                                FileExtension = Path.GetExtension(file.FileName),
                                Size = reducedFile.Length,
                            };

                            imageModels.Add(newphoto);
                        }
                        else
                        {
                            //ModelState.AddModelError("File", "The file is too large.");
                        }

                        File.Delete(outputPath);

                    }


                }

            }

            List<Infrastructure.Data.Models.Image> imagesToReturn = new List<Infrastructure.Data.Models.Image>();

            foreach (var item in imageModels)
            {
                Infrastructure.Data.Models.Image image = new Infrastructure.Data.Models.Image()
                {
                    Bytes = item.Bytes,
                    Description = item.Description,
                    FileExtension = item.FileExtension,
                    Size = item.Size,
                    PlaceId = item.PlaceId,
                    AuthorId = userId
                };

                imagesToReturn.Add(image);
            }

            return imagesToReturn;

        }

        public async Task<List<Infrastructure.Data.Models.Image>> GetImagesByIdAsync(int id, string entityType)
        {
            List<Infrastructure.Data.Models.Image> images = new();

            if (entityType == "Place")
            {
                images = await repository.All<Infrastructure.Data.Models.Image>()
                                                .Where(x => x.PlaceId == id)
                                                .ToListAsync();
            }
            else if (entityType == "Event")
            {
                images = await repository.All<Infrastructure.Data.Models.Image>()
                                .Where(x => x.EventId == id)
                                .ToListAsync();
            }

            return images;

        }

        public async Task<Dictionary<int, string>> ConvertImagesToStringAsync(List<Infrastructure.Data.Models.Image> images)
        {
            Dictionary<int, string> imagesToShow = new Dictionary<int, string>();

            foreach (var image in images)
            {
                string base64String = Convert.ToBase64String(image.Bytes);
                string imageDataURL = $"data:image/png;base64,{base64String}";
                imagesToShow.Add(image.Id, imageDataURL);
            }

            return imagesToShow;

        }

        public async Task<string> ConvertImageToStringAsync(Infrastructure.Data.Models.Image image)
        {
            string base64String = Convert.ToBase64String(image.Bytes);
            string imageDataURL = $"data:image/png;base64,{base64String}";

            return imageDataURL;
        }

        public async Task DeleteImage(int imageId)
        {

            Infrastructure.Data.Models.Image? image = await repository.All<Infrastructure.Data.Models.Image>()
                .Where(x => x.Id == imageId ).FirstOrDefaultAsync();

            if (image != null)
            {
                await repository.RemoveAsync(image);
                await repository.SaveChangesAsync();
            }
        }




        private bool isPhoto(string fileExtention)
        {
            return fileExtention == ".jpg"
                || fileExtention == ".jpeg"
                || fileExtention == ".png"
                || fileExtention == ".bmp";
        }

        public async Task SaveImagesToEventAsync(IFormFileCollection files, int eventId, string userId)
        {
            Event currentEvent = await eventService.GetEventByIdAsync(eventId);

            IList<Infrastructure.Data.Models.Image> images = await GetImagesFromViewModelAsync(files, userId);

            if (currentEvent != null)
            {
                currentEvent.Images = images;
                await repository.SaveChangesAsync();
            }
        }

        public async Task<Infrastructure.Data.Models.Image> ConvertFileToImageAsync(IFormFile file)
        {
            ImageViewModel newphoto;

            if (file != null)
            {

                using (var memoryStream = new MemoryStream())
                {

                    var outputPath = Path.Combine("uploads", file.FileName);
                    await CompressAndSaveImageAsync(file, outputPath, 30);

                    string fileExtention = Path.GetExtension(file.FileName.ToLower());
                    byte[] reducedFile = File.ReadAllBytes(outputPath);

                    await file.CopyToAsync(memoryStream);


                    if (reducedFile.Length < 2097152 && isPhoto(fileExtention))
                    {
                        newphoto = new ImageViewModel()
                        {
                            Bytes = reducedFile,
                            Description = file.FileName,
                            FileExtension = Path.GetExtension(file.FileName),
                            Size = reducedFile.Length,
                           
                        };

                        Infrastructure.Data.Models.Image image = new Infrastructure.Data.Models.Image()
                        {
                            Bytes = newphoto.Bytes,
                            Description = newphoto.Description,
                            FileExtension = newphoto.FileExtension,
                            Size = newphoto.Size,
                        };

                        File.Delete(outputPath);
                        return image;
                    }
                    else
                    {
                        //ModelState.AddModelError("File", "The file is too large.");
                    }

                }

            }

            return null;
            
        }

        public async Task AddNewUserAvatar(UserViewModel model, string userId)
        {
            ApplicationUser? currentUser = await repository.All<ApplicationUser>()
                                                    .Where(ap => ap.Id == userId)
                                                    .FirstOrDefaultAsync();

            Infrastructure.Data.Models.Image? currentAvatar = await repository.All<Infrastructure.Data.Models.Image>()
                                                                    .Where(i => i.UserId == userId)
                                                                    .FirstOrDefaultAsync();

            if (currentAvatar != null)
            {
                await repository.RemoveAsync(currentAvatar);
                await repository.SaveChangesAsync();
            }


            if (currentUser != null && model.File != null)
            {
                Infrastructure.Data.Models.Image newAvatar = await ConvertFileToImageAsync(model.File);
                newAvatar.UserId = userId;
                currentUser.Avatar = newAvatar;
                await repository.SaveChangesAsync();
            }
        }

        public async Task<string> ConvertBytesToStringAsync(byte[] bytes)
        {
            string base64String = Convert.ToBase64String(bytes);
            string imageDataURL = $"data:image/png;base64,{base64String}";

            return imageDataURL;
        }

        public async Task<bool> CheckIfUserIsTheImageAuthor(string userId, int imageId)
        {
            return await repository.AllReadOnly<Infrastructure.Data.Models.Image>()
                                    .Where(i => i.Id == imageId
                                    && i.AuthorId == userId)
                                    .AnyAsync();
        }
    }
}
