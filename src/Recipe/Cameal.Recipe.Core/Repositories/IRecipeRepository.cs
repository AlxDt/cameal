namespace Cameal.Recipe.Core.Repositories;

public interface IRecipeRepository
{
    Task<Recipe?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Recipe>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Recipe recipe, CancellationToken cancellationToken = default);
    Task UpdateAsync(Recipe recipe, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
