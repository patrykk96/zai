using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.Dto
{
    public class MovieDto : BaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public int UserRating { get; set; }
        public double UsersAverage { get; set; }
        public bool IsFavourite { get; set; }
    }
}