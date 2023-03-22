using System.ComponentModel.DataAnnotations;

namespace TicketingSystemIT.Entities.Resources;

public class CategorySaveResource
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Range(0, 4320)]
    public int EstimatedTimeInMinutes { get; set; }
}
