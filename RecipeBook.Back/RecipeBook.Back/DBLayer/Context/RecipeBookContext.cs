using DBLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DBLayer.Context;

public class RecipeBookContext : DbContext
{
    private readonly string connectionString = string.Empty;

    public RecipeBookContext(DbContextOptions<RecipeBookContext> options) : base(options) 
    {
        Database.EnsureCreated();
    }

  
    public DbSet<User> Users { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Recipe>()
            .HasOne(x => x.User)
            .WithMany(x => x.Recipes)
            .HasForeignKey(x => x.UserId);

        base.OnModelCreating(builder);
    }
}
