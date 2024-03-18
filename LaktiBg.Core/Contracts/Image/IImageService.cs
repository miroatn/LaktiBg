﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Contracts.Image
{
    public interface IImageService
    {
        Task CompressAndSaveImageAsync(IFormFile imageFile, string outputPath, int quality);
    }
}
