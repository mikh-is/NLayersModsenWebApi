using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.BLL.DTO
{
    public class AddEventDTO
    {
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Plan { get; set; }
        [Required]
        public string? Organizer { get; set; }
        [Required]
        public string? Speaker { get; set; }
        [Required]
        public DateTime? DateOfEvent { get; set; }
        [Required]
        public string? Place { get; set; }
    }
}
