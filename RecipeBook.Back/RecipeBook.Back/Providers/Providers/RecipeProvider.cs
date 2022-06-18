using DBLayer.Context;
using DBLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Providers.Providers;

public class RecipeProvider : IRecipeProvider
{
    private readonly RecipeBookContext recipeBookContext;
    public RecipeProvider(RecipeBookContext recipeBookContext) =>
        this.recipeBookContext = recipeBookContext;

    public async Task<Guid> AddAsync(Recipe model)
    {
        var id = Guid.NewGuid();

        model.Id = id;

        await recipeBookContext.AddAsync(model);
        await recipeBookContext.SaveChangesAsync();

        return id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var recipe = await GetRecipeById(id, recipeBookContext);

        if (recipe is not null)
        {
            recipeBookContext.Remove(recipe);
            await recipeBookContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<IReadOnlyCollection<Recipe>> GetAllAsyns() =>
        await recipeBookContext.Recipes.ToListAsync();


    public async Task<Recipe?> GetAsync(Guid id) => 
        await GetRecipeById(id, recipeBookContext);

    public async Task<bool> UpdateAsync(Recipe model, Guid id)
    {
        var recipe = await GetRecipeById(id, recipeBookContext);

        if (recipe is null)
            return false;

        recipe.RecipeName = model.RecipeName;
        recipe.Description = model.Description;
        recipe.Image = model.Image;
        recipe.UserId = model.UserId;

        recipeBookContext.Recipes.Update(recipe);
        
        await recipeBookContext.SaveChangesAsync();

        return true;
    }


    private static async Task<Recipe?> GetRecipeById(Guid id, RecipeBookContext recipeBookContext) => 
        await recipeBookContext.Recipes.FindAsync(id);
}
