using Microsoft.EntityFrameworkCore;
using TicketingSystemIT.Core;
using TicketingSystemIT.Entities.Models;

namespace TicketingSystemIT.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly TicketingSystemDbContext context;

    public CategoryRepository(TicketingSystemDbContext context)
    {
        this.context = context;
    }
    public void Add(Category category)
    {
        context.Add(category);
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategory(int id)
    {
        return await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public void Remove(Category category)
    {
        context.Remove(category);
    }
}
