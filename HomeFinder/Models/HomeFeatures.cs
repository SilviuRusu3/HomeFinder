using HomeFinder.HelpClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class HomeFeatures
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Name is too short")]
        public string Name { get; set; }
        public HomeType? HomeType { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
