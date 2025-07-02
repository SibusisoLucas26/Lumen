using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Model
{
    public class User : IdentityUser {
          [MaxLength(100)]
        public string DisplayName { get; set; } = string.Empty;

        
     //   [Url]
       // public string AvatarUrl { get; set; } = string.Empty;
    }
}