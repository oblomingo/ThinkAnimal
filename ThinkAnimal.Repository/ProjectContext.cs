using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ThinkAnimal.Model;

namespace ThinkAnimal.Repository
{
    public class ProjectContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Feature>()
            //            .HasKey(t => t.AnimalId);

            //modelBuilder.Entity<Feature>()
            //    .HasRequired(t => t.Animal)
            //    .WithRequiredPrincipal(t => t.Feature)
            //    .WillCascadeOnDelete(true);

            modelBuilder.Entity<Feature>()
                .HasOptional(c => c.ChildFeatureForYes)
                .WithMany()
                .Map(m => m.MapKey("ChildFeatureForYesId"));

            modelBuilder.Entity<Feature>()
                .HasOptional(c => c.ChildFeatureForNo)
                .WithMany()
                .Map(m => m.MapKey("ChildFeatureForNoId"));

            //modelBuilder.Entity<Feature>()
            //    .HasOptional(x => x.Animal)
            //    .WithRequired(x => x.Feature)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Feature>()
            //            .HasOptional(c => c.ChildFeatureForYes)
            //            .WithMany(c => c.ChildContent)
            //            .HasForeignKey(c => c.ParentContentId);

        }
    }
}
