using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.Dto
{
    public class ListCommentDto : BaseDto
    {
        public List<CommentDto> List { get; set; }
    }
}
