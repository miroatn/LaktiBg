﻿using LaktiBg.Core.Models.ImageModels;
using LaktiBg.Core.Models.UserModels;
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

        Task<IList<Image>> GetImagesFromViewModelAsync(IFormFileCollection files, string userId);

        Task<string> ConvertImageToStringAsync(Image image);

        Task SaveImagesToEventAsync(IFormFileCollection files, int eventId, string userId);

        Task<Image> ConvertFileToImageAsync(IFormFile file);

        Task AddNewUserAvatar(UserViewModel model, string userId);

        Task<string> ConvertBytesToStringAsync(byte[] bytes);

        Task<bool> CheckIfUserIsTheImageAuthor(string userId, int imageId);
    }
}
