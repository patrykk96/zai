
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
    }
}
