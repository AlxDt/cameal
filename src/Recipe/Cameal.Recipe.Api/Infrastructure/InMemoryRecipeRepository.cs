using Cameal.Recipe.Core;
using Cameal.Recipe.Core.Repositories;
using System.Collections.Concurrent;

namespace Cameal.Recipe.Api.Infrastructure;

public class InMemoryRecipeRepository : IRecipeRepository
{
    private readonly ConcurrentDictionary<Guid, Core.Recipe> _recipes = new();

    public Task<Core.Recipe?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _recipes.TryGetValue(id, out var recipe);
        return Task.FromResult(recipe);
    }

    public Task<IEnumerable<Core.Recipe>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<Core.Recipe>>(_recipes.Values);
    }

    public Task AddAsync(Core.Recipe recipe, CancellationToken cancellationToken = default)
    {
        _recipes.TryAdd(recipe.Id, recipe);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Core.Recipe recipe, CancellationToken cancellationToken = default)
    {
        _recipes[recipe.Id] = recipe;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _recipes.TryRemove(id, out _);
        return Task.CompletedTask;
    }
}
