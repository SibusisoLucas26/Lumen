using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName {get; set;}
    }
}