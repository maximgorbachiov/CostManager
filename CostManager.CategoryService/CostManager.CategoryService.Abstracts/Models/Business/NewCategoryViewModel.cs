using System.Text.Json.Serialization;

namespace CostManager.CategoryService.Abstracts.Models.Business;

public class NewCategoryViewModel
{
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("parentCategoryId")]
    public Guid? ParentCategoryId { get; set; }
}