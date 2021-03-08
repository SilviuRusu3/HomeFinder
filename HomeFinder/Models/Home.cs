using HomeFinder.HelpClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class Home
    {
        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        [Required]
        public HomeType? HomeType { get; set; }
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public double? GeneralGrade { get; set; }
        [Required]
        [StringLength(100)]
        public string Adress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        [StringLength(300)]
        public string Review { get; set; }
        public int AreaId { get; set; }
        [ForeignKey("AreaId")]
        public Area Area { get; set; }
        public ICollection<GradedFeatures> GradedFeaturee { get; set; }
    }
}
