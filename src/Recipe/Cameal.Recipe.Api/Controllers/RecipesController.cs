using Cameal.Recipe.Core;
using Cameal.Recipe.Core.Repositories;
using Cameal.Recipe.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cameal.Recipe.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeRepository _repository;
    private readonly IGroceryListService _groceryListService;

    public RecipesController(IRecipeRepository repository, IGroceryListService groceryListService)
    {
        _repository = repository;
        _groceryListService = groceryListService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Core.Recipe>>> GetAll()
    {
        var recipes = await _repository.GetAllAsync();
        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Core.Recipe>> GetById(Guid id)
    {
        var recipe = await _repository.GetByIdAsync(id);
        
        if (recipe == null)
        {
            return NotFound();
        }
        
        return Ok(recipe);
    }

    [HttpPost]
    public async Task<ActionResult<Core.Recipe>> Create(CreateRecipeRequest request)
    {
        var recipe = new Core.Recipe(Guid.NewGuid(), request.Name, request.Description);
        
        if (request.Ingredients != null)
        {
            foreach (var ingredient in request.Ingredients)
            {
                recipe.AddIngredient(ingredient);
            }
        }
        
        if (request.Steps != null)
        {
            foreach (var step in request.Steps)
            {
                recipe.AddStep(step);
            }
        }
        
        await _repository.AddAsync(recipe);
        
        return CreatedAtAction(nameof(GetById), new { id = recipe.Id }, recipe);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateRecipeRequest request)
    {
        var recipe = await _repository.GetByIdAsync(id);
        
        if (recipe == null)
        {
            return NotFound();
        }
        
        recipe.UpdateDetails(request.Name, request.Description);
        
        // Clear and re-add ingredients
        foreach (var ingredient in recipe.Ingredients.ToList())
        {
            recipe.RemoveIngredient(ingredient);
        }
        
        if (request.Ingredients != null)
        {
            foreach (var ingredient in request.Ingredients)
            {
                recipe.AddIngredient(ingredient);
            }
        }
        
        // Clear and re-add steps
        foreach (var step in recipe.Steps.ToList())
        {
            recipe.RemoveStep(step);
        }
        
        if (request.Steps != null)
        {
            foreach (var step in request.Steps)
            {
                recipe.AddStep(step);
            }
        }
        
        await _repository.UpdateAsync(recipe);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var recipe = await _repository.GetByIdAsync(id);
        
        if (recipe == null)
        {
            return NotFound();
        }
        
        await _repository.DeleteAsync(id);
        
        return NoContent();
    }

    [HttpPost("grocery-list")]
    public async Task<ActionResult<IEnumerable<GroceryListItem>>> GenerateGroceryList(GenerateGroceryListRequest request)
    {
        var recipes = new List<Core.Recipe>();
        
        foreach (var recipeId in request.RecipeIds)
        {
            var recipe = await _repository.GetByIdAsync(recipeId);
            
            if (recipe != null)
            {
                recipes.Add(recipe);
            }
        }
        
        var groceryList = _groceryListService.GenerateGroceryList(recipes);
        
        return Ok(groceryList);
    }
}

public record CreateRecipeRequest(
    string Name,
    string Description,
    List<Ingredient>? Ingredients,
    List<Step>? Steps);

public record UpdateRecipeRequest(
string Name,
string Description,
List<Ingredient>? Ingredients,
List<Step>? Steps);

public record GenerateGroceryListRequest(
    List<Guid> RecipeIds);
