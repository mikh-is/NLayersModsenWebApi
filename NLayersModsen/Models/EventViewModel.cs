using System.ComponentModel.DataAnnotations;

namespace NLayerApp.WEB.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        [Required]
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
