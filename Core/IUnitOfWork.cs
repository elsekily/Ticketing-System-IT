namespace TicketingSystemIT.Core;

public interface IUnitOfWork
{
    Task CompleteAsync();
}