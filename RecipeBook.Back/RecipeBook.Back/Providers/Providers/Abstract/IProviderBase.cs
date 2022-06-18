using DBLayer.Models;

namespace Providers.Providers;

public interface IProviderBase<TModel> where TModel : ModelBase
{
    Task<Guid> AddAsync(TModel model);
    Task<TModel?> GetAsync(Guid id);
    Task<IReadOnlyCollection<TModel>> GetAllAsyns();
    Task<bool> UpdateAsync(TModel model, Guid id);
    Task<bool> DeleteAsync(Guid id);
}
