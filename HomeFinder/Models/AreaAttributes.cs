using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class AreaAttributes
    {
        public int LocationAttributesId { get; set; }
        public LocationAttributes LocationAttributes { get; set; }
        [ForeignKey("LocationAttributesId")]
        public int AreaId { get; set; }
        [ForeignKey("AreaId")]
        public Area Area { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public double Grade { get; set; }
    }
}
