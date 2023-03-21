using System.ComponentModel.DataAnnotations;

namespace TicketingSystemIT.Entities.Resources;

public class SaveUserResource
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}