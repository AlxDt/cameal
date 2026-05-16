namespace Cameal.Client.Web.Models;

public record GenerateGroceryListRequest(
    List<Guid> RecipeIds);
