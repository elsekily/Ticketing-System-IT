namespace TicketingSystemIT.Entities.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime TimeIssued { get; set; }
    public DateTime TimeReceived { get; set; }
    public DateTime TimeSolved { get; set; }

    public int UserIssuedId { get; set; }
    public User UserIssued { get; set; }

    public int AssignedEmployeeID { get; set; }
    public User AssignedEmployee { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
