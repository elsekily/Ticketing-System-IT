using Microsoft.AspNetCore.Identity;

namespace TicketingSystemIT.Entities.Models;

public class User : IdentityUser<int>
{
    public ICollection<UserRole> Roles { get; set; }
    public ICollection<Ticket> TicketsIssued { get; set; }
    public ICollection<Ticket> TicketsSolved { get; set; }
}