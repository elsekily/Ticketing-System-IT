namespace TicketingSystemIT.Entities.Models;

public class Category
{
    public int Id { get; set; }
    public string Description { get; set; }
    public TimeSpan EstimatedTimeInMinutes { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}