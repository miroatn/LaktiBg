using LaktiBg.Core.Models.ImageModels;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;

namespace LaktiBg.Core.Contracts.ImageService
{
    public interface IImageService
    {
        Task CompressAndSaveImageAsync(IFormFile imageFile, string outputPath, int quality);

        Task<List<Image>> GetImagesByIdAsync(int id, string entityType);

        Task<Dictionary<int, string>> ConvertImagesToStringAsync(List<Image> images);

        Task DeleteImage(int imageId);

        Task<IList<Image>> GetImagesFromViewModelAsync(IFormFileCollection files);

        Task SaveImagesToEventAsync(IFormFileCollection files, int eventId);
    }
}
