using DBLayer.Context;
using DBLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Providers.Providers;

public class UserProvider : IUserProvider
{
    private readonly RecipeBookContext recipeBookContext;
    public UserProvider(RecipeBookContext recipeBookContext) =>
        this.recipeBookContext = recipeBookContext;

    public async Task<Guid> AddAsync(User model)
    {
        var id = Guid.NewGuid();

        model.Id = id;

        await recipeBookContext.AddAsync(model);
        await recipeBookContext.SaveChangesAsync();

        return id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await GetUserById(id, recipeBookContext);

        if (user is not null)
        {
            recipeBookContext.Users.Remove(user);
            await recipeBookContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<IReadOnlyCollection<User>> GetAllAsyns() =>
        await recipeBookContext.Users.ToListAsync();

    public async Task<User?> GetAsync(Guid id) =>
        await GetUserById(id, recipeBookContext);   
    

    public async Task<bool> UpdateAsync(User model, Guid id)
    {
        var user = await GetUserById(id, recipeBookContext);

        if (user is null)
            return false;

        user.Name = model.Name;
        user.Login = model.Login;
        user.Password = model.Password;

        await recipeBookContext.SaveChangesAsync(); 

        recipeBookContext.Update(user);

        return true;
    }

    private static async Task<User?> GetUserById(Guid id, RecipeBookContext recipeBookContext) =>
        await recipeBookContext.Users.FindAsync(id);
}
