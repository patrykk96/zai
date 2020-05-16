using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.Models
{
    public class ReviewModel
    {
        public int Score { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }

    }
}
