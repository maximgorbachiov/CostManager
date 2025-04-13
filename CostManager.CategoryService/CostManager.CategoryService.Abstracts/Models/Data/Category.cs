using Newtonsoft.Json;

namespace CostManager.CategoryService.Abstracts.Models.Data;

public class Category
{
    [JsonProperty("id")]
    public Guid CategoryId { get; set; }
    
    [JsonProperty("userId")]
    public Guid UserId { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}