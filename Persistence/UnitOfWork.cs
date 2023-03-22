using TicketingSystemIT.Core;

namespace TicketingSystemIT.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly TicketingSystemDbContext context;

    public UnitOfWork(TicketingSystemDbContext context)
    {
        this.context = context;
    }
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}