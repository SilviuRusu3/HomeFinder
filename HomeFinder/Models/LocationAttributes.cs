using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class LocationAttributes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Name is too short")]
        public string Name { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int Rank { get; set; }
        [Range(1,10)]
        public double? Grade { get; set; }
        public ICollection<AreaAttributes> AreaAttributes { get; set; }

    }
}
