using System.ComponentModel.DataAnnotations;

namespace TicketingSystemIT.Entities.Resources;

public class TicketSaveResource
{
    [Required]
    public string Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
}