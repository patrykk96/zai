using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.Dto
{
    public class ResultDto<T> where T : BaseDto
    {
        public T SuccessResult { get; set; }
        public string Error { get; set; }
    }
}
