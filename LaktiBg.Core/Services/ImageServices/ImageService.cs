using LaktiBg.Core.Contracts.Image;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Services.ImageServices
{
    public class ImageService : IImageService
    {
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
    }
}
