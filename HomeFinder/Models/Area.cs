using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class Area
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public double? GeneralGrade { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<AreaAttributes> AreaAttributes { get; set; }
        public ICollection<Home> Homes { get; set; }
    }
}
