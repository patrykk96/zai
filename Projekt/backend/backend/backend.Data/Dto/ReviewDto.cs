using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.Dto
{
    public class ReviewDto : BaseDto
    {
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public string Author { get; set; }
        public int MovieId { get; set; }
    }
}
