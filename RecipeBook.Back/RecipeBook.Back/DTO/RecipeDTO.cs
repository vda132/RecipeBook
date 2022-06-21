using System.ComponentModel.DataAnnotations;

namespace DTO;

public class RecipeDTO
{
    [Required]
    public string Image { get; set; }

    [Required]
    public string RecipeName { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public Guid UserId { get; set; }
}
