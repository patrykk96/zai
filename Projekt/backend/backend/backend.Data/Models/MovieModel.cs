using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.Models
{
    public class MovieModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Logo { get; set; }
    }
}