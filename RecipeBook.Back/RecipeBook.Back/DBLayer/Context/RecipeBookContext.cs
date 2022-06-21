using DBLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Context;

public class RecipeBookContext : DbContext
{
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
