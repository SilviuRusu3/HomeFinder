using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class GradedFeatures
    {
        public int HomeFeaturesId { get; set; }
        public HomeFeatures HomeFeatures { get; set; }
        [ForeignKey("HomeFeaturesId")]
        public int HomeId { get; set; }
        [ForeignKey("HomeId")]
        public Home Home { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public double Grade { get; set; }
    }
}
