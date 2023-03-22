namespace TicketingSystemIT.Entities.Resources;

public class TicketForEmployeeWithCategoryResource
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int CategoryName { get; set; }
    public DateTime TimeIssued { get; set; }
    public bool IsAssigned { get; set; }
    public DateTime? TimeSolved { get; set; }
    public string AssignedEmployeeName { get; set; }
}