using backend.Data.Dto;
using backend.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    public interface IImageService
    {
        Task<FileStream> GetImage(string filename);
    }
}