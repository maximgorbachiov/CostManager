using System.Collections.Concurrent;
using CostManager.CategoryService.Abstracts.Interfaces.Data;
using CostManager.CategoryService.Abstracts.Models.Data;
using CostManager.CategoryService.Repositories.Extensions;

namespace CostManager.CategoryService.Repositories;

public class InMemoryRepository : ICategoryRepository
{
    private readonly ConcurrentDictionary<string, UserCategoryGroup> _usersCategoriesGroups = new();
    
    public async Task<string> AddCategory(Category category)
    {
        if (string.IsNullOrEmpty(category.UserId))
            throw new ArgumentException($"UserId can't be null or empty: {category.UserId}");
            
        var userCategories = _usersCategoriesGroups.GetOrAdd(category.UserId, new UserCategoryGroup());

        _ = userCategories.TryAdd(category);
        
        return await Task.FromResult(category.CategoryId);
    }

    public async Task<bool> UpdateCategory(Category category)
    {
        if (string.IsNullOrEmpty(category.UserId))
            throw new ArgumentException($"UserId can't be null or empty: {category.UserId}");

        bool result = false;
        
        if (_usersCategoriesGroups.TryGetValue(category.UserId, out var userCategoryGroup))
        {
            result = userCategoryGroup.TryUpdateCategory(category);
        }
        
        return await Task.FromResult(result);
    }

    public async Task<Category?> GetCategoryById(string userId, string categoryId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException($"UserId can't be null or empty: {userId}");
        
        Category? category = null;
        
        if (_usersCategoriesGroups.TryGetValue(userId, out var userCategories))
        {
            category = userCategories.GetCategoryById(categoryId);
        }
        
        return await Task.FromResult(category);
    }
    
    public async Task<Category?> GetCategoryByName(string userId, string categoryName)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException($"UserId can't be null or empty: {userId}");
        
        Category? category = null;
        
        if (_usersCategoriesGroups.TryGetValue(userId, out var userCategories))
        {
            category = userCategories.GetCategoryByName(categoryName);
        }
        
        return await Task.FromResult(category);
    }

    public async Task<List<Category>?> GetCategories(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException($"UserId can't be null or empty: {userId}");
        
        List<Category>? categories = null;
        
        if (_usersCategoriesGroups.TryGetValue(userId, out var userCategories))
        {
            categories = userCategories.GetCategories();
        }
        
        return await Task.FromResult(categories);;
    }
    
    public async Task<CategoryWithChildren?> GetCategoryWithChildren(string userId, string categoryId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException($"UserId can't be null or empty: {userId}");
        
        CategoryWithChildren? category = null;
        
        if (_usersCategoriesGroups.TryGetValue(userId, out var userCategories))
        {
            category = userCategories.GetCategoryWithChildren(categoryId);
        }
        
        return await Task.FromResult(category);
    }

    public async Task<bool> RemoveCategory(string userId, string categoryId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException($"UserId can't be null or empty: {userId}");
        
        bool result = false;
        
        if (_usersCategoriesGroups.TryGetValue(userId, out var userCategories))
        {
            result = userCategories.TryRemove(categoryId);
        }
        
        return await Task.FromResult(result);
    }

    public async Task<bool> RemoveCategories(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException($"UserId can't be null or empty: {userId}");

        bool result = _usersCategoriesGroups.TryRemove(userId, out var userCategories);
        
        return await Task.FromResult(result);
    }

    private class UserCategoryGroup
    {
        private readonly ConcurrentDictionary<string, Category> _categoriesById = new();

        public bool TryAdd(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
            
            if (!string.IsNullOrEmpty(category.CategoryId))
                throw new ArgumentException($"Category should not exist. CategoryId should be null or empty: {category.CategoryId}");
            
            if (string.IsNullOrEmpty(category.Title))
                throw new ArgumentException($"Title can't be null or empty: {category.Title}");

            category.CategoryId = Guid.NewGuid().ToString();
            return _categoriesById.TryAdd(category.CategoryId, category);
        }

        public bool TryUpdateCategory(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
            
            if (string.IsNullOrEmpty(category.CategoryId))
                throw new ArgumentException($"CategoryId can't be null or empty: {category.CategoryId}");
            
            if (string.IsNullOrEmpty(category.Title))
                throw new ArgumentException($"Title can't be null or empty: {category.Title}");

            bool result = false;
            
            if (_categoriesById.TryGetValue(category.CategoryId, out var oldCategory))
            {
                oldCategory.Update(category);
                result = true;
            }

            return result;
        }

        public Category? GetCategoryById(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
                throw new ArgumentException($"CategoryId can't be null or empty: {categoryId}");
            
            return _categoriesById.GetValueOrDefault(categoryId);
        }
        
        public Category? GetCategoryByName(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
                throw new ArgumentException($"categoryName can't be null or empty: {categoryName}");

            return _categoriesById.FirstOrDefault(c => c.Value.Title == categoryName).Value;
        }
        
        public List<Category>? GetCategories()
        {
            return _categoriesById.Values.ToList();
        }
        
        public CategoryWithChildren? GetCategoryWithChildren(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
                throw new ArgumentException($"CategoryId can't be null or empty: {categoryId}");
            
            var category = GetCategoryById(categoryId);
            
            if (category == null) return null;

            var categoryWithChildren = category.ToCategoryWithChildren();
            
            var children = _categoriesById
                .Where(c => c.Value.ParentCategoryId == categoryId)
                .Select(c => c.Value);

            foreach (var child in children)
            {
                var childWithChildren = GetCategoryWithChildren(child.CategoryId);

                if (childWithChildren != null)
                {
                    categoryWithChildren.ChildrenCategories.Add(childWithChildren);   
                }
            }
            
            return categoryWithChildren;
        }
        
        public bool TryRemove(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
                throw new ArgumentException($"CategoryId can't be null or empty: {categoryId}");

            bool result = _categoriesById.TryRemove(categoryId, out _);
            
            return result;
        }
    }
}