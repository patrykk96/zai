
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Data.DbModels
{
    public class User : IdentityUser
    {
        public int UserStatus { get; set; }
        public string Description { get; set; }

        public virtual ICollection<FavouriteMovie> FavouriteMovies { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
