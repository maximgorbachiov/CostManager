using Abstraction.Repositories;
using Data.Models;

namespace Implementation.Repository.Postgres
{
    public class CategoryRepository : ICategoryRepository
    {
        private CategoryContext _context;

        public CategoryRepository(CategoryContext context)
        {
            _context = context;
        }

        public Category CreateTrackedEntity(Category entity)
        {
            throw new NotImplementedException();
        }

        public Guid CreateUntrackedEntity(Category entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntities(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntities(Func<Category> selector)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntity(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetTrackedEntities()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetTrackedEntities(Func<Category> selector)
        {
            throw new NotImplementedException();
        }

        public Category GetTrackedEntity(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetUntrackedEntities()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetUntrackedEntities(Func<Category> selector)
        {
            throw new NotImplementedException();
        }

        public Category GetUntrackedEntity(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool IsEntityExist(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}