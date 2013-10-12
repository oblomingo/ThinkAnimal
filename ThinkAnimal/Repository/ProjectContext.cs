using System.Data.Entity;
using ThinkAnimal.Models;

namespace ThinkAnimal.Repository
{
    public class ProjectContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; } 
        public DbSet<Feature> Features { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}