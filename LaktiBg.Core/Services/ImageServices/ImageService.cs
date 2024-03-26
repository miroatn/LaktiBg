using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Models.ImageModels;
using LaktiBg.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace LaktiBg.Core.Services.ImageServices
{
    public class ImageService : IImageService
    {
        private readonly IRepository repository;

        public ImageService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CompressAndSaveImageAsync(IFormFile imageFile, string outputPath, int quality)
        {
            using var imageStream = imageFile.OpenReadStream();
            using var image = Image.Load(imageStream);
            var encoder = new JpegEncoder
            {
                Quality = quality, // Adjust this value for desired compression quality
            };

            await Task.Run(() => image.Save(outputPath, encoder));

        }

        public async Task<IList<Infrastructure.Data.Models.Image>> GetImagesFromViewModelAsync(IFormFileCollection files)
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

        public async Task DeleteImage(int imageId)
        {

            Infrastructure.Data.Models.Image? image = await repository.All<Infrastructure.Data.Models.Image>()
                .Where(x => x.Id == imageId).FirstOrDefaultAsync();

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

    }
}
