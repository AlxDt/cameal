using System.Net.Http.Json;
using Cameal.Client.Web.Models;

namespace Cameal.Client.Web.Services;

public class RecipeApiClient : IRecipeApiClient
{
    private readonly HttpClient _httpClient;

    public RecipeApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<RecipeDto>> GetAllRecipesAsync()
    {
        var recipes = await _httpClient.GetFromJsonAsync<IEnumerable<RecipeDto>>("api/recipes");
        return recipes ?? Array.Empty<RecipeDto>();
    }

    public async Task<RecipeDto?> GetRecipeByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<RecipeDto>($"api/recipes/{id}");
    }

    public async Task<RecipeDto> CreateRecipeAsync(CreateRecipeRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/recipes", request);
        response.EnsureSuccessStatusCode();
        var recipe = await response.Content.ReadFromJsonAsync<RecipeDto>();
        return recipe!;
    }

    public async Task UpdateRecipeAsync(Guid id, UpdateRecipeRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/recipes/{id}", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteRecipeAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/recipes/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<GroceryListItemDto>> GenerateGroceryListAsync(GenerateGroceryListRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/recipes/grocery-list", request);
        response.EnsureSuccessStatusCode();
        var groceryList = await response.Content.ReadFromJsonAsync<IEnumerable<GroceryListItemDto>>();
        return groceryList ?? Array.Empty<GroceryListItemDto>();
    }
}
