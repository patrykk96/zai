using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.DbModels
{
    public class Movie : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<FavouriteMovie> FavouriteMovies { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
