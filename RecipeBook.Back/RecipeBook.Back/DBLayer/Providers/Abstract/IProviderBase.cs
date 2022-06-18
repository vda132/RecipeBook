namespace DBLayer.Providers;

public interface IProviderBase<TModel> where TModel : Models.ModelBase
{
    Task<Guid> AddAsync(TModel model);
    Task<TModel?> GetAsync(int id);
    Task<IReadOnlyCollection<TModel>> GetAllAsyns();
    Task<bool> UpdateAsync(TModel model, int id);
    Task<bool> DeleteAsync(int id);
}
