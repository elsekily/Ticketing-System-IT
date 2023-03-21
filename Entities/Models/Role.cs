using Microsoft.AspNetCore.Identity;

namespace TicketingSystemIT.Entities.Models;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole> User { get; set; }
}
