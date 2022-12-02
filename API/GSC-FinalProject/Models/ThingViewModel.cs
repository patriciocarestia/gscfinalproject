using System.ComponentModel.DataAnnotations;

namespace GSC_FinalProject.Models
{
    public class ThingViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
    }
}
