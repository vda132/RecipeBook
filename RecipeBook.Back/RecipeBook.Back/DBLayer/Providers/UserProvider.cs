using DBLayer.Models;

namespace DBLayer.Providers;

public class UserProvider : IProviderBase<User>
{
    public Task<Guid> AddAsync(User model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<User>> GetAllAsyns()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(User model, int id)
    {
        throw new NotImplementedException();
    }
}
