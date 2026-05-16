namespace Cameal.Recipe.Core.Services;

public class GroceryListService : IGroceryListService
{
    public IEnumerable<GroceryListItem> GenerateGroceryList(IEnumerable<Recipe> recipes)
    {
        var ingredientGroups = recipes
            .SelectMany(r => r.Ingredients)
            .GroupBy(i => new { i.Name, i.Unit });

        var groceryList = new List<GroceryListItem>();

        foreach (var group in ingredientGroups)
        {
            var totalQuantity = group.Sum(i => i.Quantity);
            groceryList.Add(new GroceryListItem(
                group.Key.Name,
                totalQuantity,
                group.Key.Unit));
        }

        return groceryList;
    }
}
