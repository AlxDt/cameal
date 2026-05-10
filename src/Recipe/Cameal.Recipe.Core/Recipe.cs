namespace Cameal.Recipe.Core;

internal class Recipe
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    private readonly List<Ingredient> _ingredients = [];
    private readonly List<Step> _steps = [];

    public IReadOnlyCollection<Ingredient> Ingredients => _ingredients.AsReadOnly();
    public IReadOnlyCollection<Step> Steps => _steps.AsReadOnly();

    public Recipe(
        Guid id,
        string name,
        string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        _ingredients.Remove(ingredient);
    }

    public void AddStep(Step step)
    {
        _steps.Add(step);
    }

    public void RemoveStep(Step step)
    {
        _steps.Remove(step);
    }

    public void UpdateDetails(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
