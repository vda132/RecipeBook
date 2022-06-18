using DBLayer.Models;

namespace DBLayer.Providers;

public class RecipeProvider : IProviderBase<Recipe>
{
    public Task<Guid> AddAsync(Recipe model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Recipe>> GetAllAsyns()
    {
        throw new NotImplementedException();
    }

    public Task<Recipe?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Recipe model, int id)
    {
        throw new NotImplementedException();
    }
}
