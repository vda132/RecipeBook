using System.ComponentModel.DataAnnotations;

namespace DBLayer.Models;

public class ModelBase
{
    [Key]
    public Guid Id { get; set; }
}
