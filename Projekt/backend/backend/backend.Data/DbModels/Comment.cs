using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.DbModels
{
    public class Comment : Entity
    {
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string AuthorId { get; set; }
        public int ReviewId { get; set; }
        
        public virtual User User { get; set; }
        public virtual Review Review { get; set; }
    }
}
