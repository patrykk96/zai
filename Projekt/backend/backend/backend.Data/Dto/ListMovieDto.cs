using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.Dto
{
    public class ListMovieDto : BaseDto
    {
        public List<MovieDto> List { get; set; }
    }
}