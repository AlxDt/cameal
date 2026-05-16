namespace Cameal.Client.Web.Models;

public record CreateRecipeRequest(
    string Name,
    string Description,
    List<IngredientDto>? Ingredients,
    List<StepDto>? Steps);
