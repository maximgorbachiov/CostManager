using Abstraction.Repositories;
using Data.Models;

namespace Implementation.Repository.InMemory
{
    public class InMemoryCategoryRepository : ICategoryRepository
    {
        private List<Category> _categories;

        public InMemoryCategoryRepository()
        {
            _categories = new List<Category>();
        }

        public Category CreateTrackedEntity(Category entity)
        {
            Category category = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryDescription = entity.CategoryDescription,
                CategoryName = entity.CategoryName,
                ParentCategoryId = entity.ParentCategoryId,
                UserId = entity.UserId
            };

            _categories.Add(category);

            return category;
        }

        public Guid CreateUntrackedEntity(Category entity)
        {
            Category category = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryDescription = entity.CategoryDescription,
                CategoryName = entity.CategoryName,
                ParentCategoryId = entity.ParentCategoryId,
                UserId = entity.UserId
            };

            _categories.Add(category);

            return category.CategoryId;
        }

        public bool DeleteEntities(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntities(Func<Category, bool> selector)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEntity(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetTrackedEntities()
        {
            return _categories;
        }

        public IEnumerable<Category> GetTrackedEntities(Func<Category, bool> selector)
        {
            return _categories.Where(selector);
        }

        public Category GetTrackedEntity(Guid id)
        {
            return _categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public IEnumerable<Category> GetUntrackedEntities()
        {
            return _categories.Select(c => new Category
            {
                CategoryId = c.CategoryId,
                CategoryDescription = c.CategoryDescription,
                CategoryName = c.CategoryName,
                ParentCategoryId = c.ParentCategoryId,
                UserId = c.UserId
            }).ToList();
        }

        public IEnumerable<Category> GetUntrackedEntities(Func<Category, bool> selector)
        {
            return GetUntrackedEntities().Where(selector);
        }

        public Category GetUntrackedEntity(Guid id)
        {
            Category category = _categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return null;
            }

            return new Category
            {
                CategoryId = category.CategoryId,
                CategoryDescription = category.CategoryDescription,
                CategoryName = category.CategoryName,
                ParentCategoryId = category.ParentCategoryId,
                UserId = category.UserId
            };
        }

        public bool IsEntityExist(Guid id)
        {
            return _categories.Any(c => c.CategoryId == id);
        }
    }
}