namespace Cameal.Client.Web.Models;

public record UpdateRecipeRequest(
    string Name,
    string Description,
    List<IngredientDto>? Ingredients,
    List<StepDto>? Steps);
