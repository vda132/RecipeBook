using DBLayer.Models;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Providers.Providers;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly IRecipeProvider recipeProvider;
    private readonly IUserProvider userProvider;

    public RecipesController(IRecipeProvider recipeProvider, IUserProvider userProvider)
    {
        this.recipeProvider = recipeProvider;
        this.userProvider = userProvider;
    }

    [HttpGet]
    public async Task<JsonResult> GetAll() =>
        new JsonResult((await this.recipeProvider.GetAllAsyns()).ToList());

    [HttpGet("{id}")]
    public async Task<JsonResult> Get(Guid id)
    {
        var recipe = await this.recipeProvider.GetAsync(id);

        var user = await this.userProvider.GetAsync(recipe!.UserId);

        var RecipeDTO = new RecipeDTO
        {
            Description = recipe.Description,
            Image = recipe.Image,
            RecipeName = recipe.RecipeName,
            UserId = recipe.UserId
        };

        return new JsonResult(RecipeDTO);
    }

    [HttpPost]
    public async Task<ResultDTO> Post([FromBody] RecipeDTO recipe)
    {
        if(string.IsNullOrEmpty(recipe.Description) ||
            string.IsNullOrEmpty(recipe.RecipeName) ||
            string.IsNullOrEmpty(recipe.UserId.ToString()) ||
            string.IsNullOrEmpty(recipe.Image))
            return new ResultDTO
            {
                Status = 500,
                Message = "Заполните поля"
            };

        var recipes = (await this.recipeProvider.GetAllAsyns()).ToList();

        if(recipes.FirstOrDefault(x => x.Image.Equals(recipe.Image)) is not null)
            return new ResultDTO
            {
                Status = 500,
                Message = "Такая картинка уже есть"
            };

        var model = new Recipe
        {
            Description = recipe.Description,
            RecipeName = recipe.RecipeName,
            UserId = recipe.UserId,
            Image = recipe.Image
        };

        await this.recipeProvider.AddAsync(model);

        return new ResultDTO
        {
            Status = 200,
            Message = "Ok"
        };
    }
}
