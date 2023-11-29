using Data.Models;

namespace Abstraction.Repositories
{
    public interface ICategoryRepository
    {
        bool IsEntityExist(Guid id);

        Category GetUntrackedEntity(Guid id);
        Category GetTrackedEntity(Guid id);

        IEnumerable<Category> GetUntrackedEntities();
        IEnumerable<Category> GetUntrackedEntities(Func<Category, bool> selector);
        IEnumerable<Category> GetTrackedEntities();
        IEnumerable<Category> GetTrackedEntities(Func<Category, bool> selector);

        Category CreateTrackedEntity(Category entity);
        Guid CreateUntrackedEntity(Category entity);

        bool DeleteEntity(Guid id);
        bool DeleteEntities(IEnumerable<Guid> ids);
        bool DeleteEntities(Func<Category, bool> selector);
    }
}