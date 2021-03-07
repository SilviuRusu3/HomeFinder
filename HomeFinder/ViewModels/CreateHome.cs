using HomeFinder.HelpClass;
using HomeFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.ViewModels
{
    public class CreateHome
    {
        public int AreaId { get; set; }
        public int HomeId { get; set; }
        public IList<HomeFeatures> HomeFeatures { get; set; }
    }
}
