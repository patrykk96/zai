using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.DbModels
{
    public class Review : Entity
    {
        public int Score { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
