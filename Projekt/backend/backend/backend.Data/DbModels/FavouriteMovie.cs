using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.DbModels
{
    public class FavouriteMovie : Entity
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }

        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
