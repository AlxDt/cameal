namespace Cameal.Client.Web.Models;

public record RecipeDto(
    Guid Id,
    string Name,
    string Description,
    List<IngredientDto> Ingredients,
    List<StepDto> Steps);
