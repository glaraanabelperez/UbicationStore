using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Models
{
    public class StoreCommand
    {
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Direction { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public decimal Latitude { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public decimal Longitude { get; set; }
    }
}
