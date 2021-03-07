using HomeFinder.HelpClass;
using HomeFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.ViewModels
{
    public class AreaHome
    {
        public Area area { get; set; }
        public HomeType homeType { get; set; }
        public IEnumerable<Home> homes { get; set; }

    }
}
