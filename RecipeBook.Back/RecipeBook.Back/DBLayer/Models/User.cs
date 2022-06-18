using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBLayer.Models;

[Table("tblUser")]
public class User : ModelBase
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Login { get; set; }
    
    [Required]
    public string Password { get; set; }

    public IReadOnlyCollection<Recipe> Recipes { get; set; }
}
