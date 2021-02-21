using HomeFinder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.ViewModels
{
    public class LocationGrades
    {
        public int AreaId { get; set; }
        public IList<LocationAttributes> LocationAttributes { get; set; }
    }
}
