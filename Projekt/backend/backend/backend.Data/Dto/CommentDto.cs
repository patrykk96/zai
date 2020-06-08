using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.Dto
{
    public class CommentDto : BaseDto
    {
        public int CommentId { get; set; }
        public string Author { get; set; }
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public string Created { get; set; }
        public bool IsUpdated { get; set; }
        public string Updated { get; set; }
    }
}
