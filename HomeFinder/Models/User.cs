using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class User : IdentityUser
    {
        public ICollection<LocationAttributes> LocationAttributes { get; set; }
        public ICollection<HomeFeatures> HomeFeatures { get; set; }
        public ICollection<Area> Areas { get; set; }
    }
}
