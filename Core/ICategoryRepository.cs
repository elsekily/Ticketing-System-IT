using TicketingSystemIT.Entities.Models;

namespace TicketingSystemIT.Core;
public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategories();
    Task<Category> GetCategory(int id);
    void Add(Category category);
    void Remove(Category category);
}
