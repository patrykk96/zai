using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.Dto
{
    public class ListReviewDto : BaseDto
    {
        public List<ReviewDto> List { get; set; }
    }
}
