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
        [Display(Name="Desired trait for location")]
        public string Name { get; set; }
        [Required]
        [Range(1,100)]
        public int Rank { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
