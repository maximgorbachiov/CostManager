using Newtonsoft.Json;

namespace CostManager.CategoryService.Abstracts.Models.Data;

public class Category
{
    [JsonProperty("id")]
    public string CategoryId { get; set; }
    [JsonProperty("userId")]
    public string UserId { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public string ParentCategoryId { get; set; }
}