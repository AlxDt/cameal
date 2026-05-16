namespace Cameal.Recipe.Core.Services;

public interface IGroceryListService
{
    IEnumerable<GroceryListItem> GenerateGroceryList(IEnumerable<Recipe> recipes);
}
