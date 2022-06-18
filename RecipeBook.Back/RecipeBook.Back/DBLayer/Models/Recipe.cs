using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBLayer.Models;

[Table("tblRecipe")]
public class Recipe : ModelBase
{
    [Required]
    public string Image { get; set; }
    
    [Required]
    public string RecipeName { get; set; }
    
    [Required]
    public string Description { get; set; }

    [ForeignKey("UserId")]
    public Guid UserId { get; set; }

    public User User { get; set; }
}
