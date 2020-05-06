using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.Models
{
    public class ImageModel
    {
        public IFormFile Image { get; set; }
    }
}