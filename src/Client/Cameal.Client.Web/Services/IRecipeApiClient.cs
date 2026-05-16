using Cameal.Client.Web.Models;

namespace Cameal.Client.Web.Services;

public interface IRecipeApiClient
{
    Task<IEnumerable<RecipeDto>> GetAllRecipesAsync();
    Task<RecipeDto?> GetRecipeByIdAsync(Guid id);
    Task<RecipeDto> CreateRecipeAsync(CreateRecipeRequest request);
    Task UpdateRecipeAsync(Guid id, UpdateRecipeRequest request);
    Task DeleteRecipeAsync(Guid id);
    Task<IEnumerable<GroceryListItemDto>> GenerateGroceryListAsync(GenerateGroceryListRequest request);
}



