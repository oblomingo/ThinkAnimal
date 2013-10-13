using System.Collections.Generic;
using System.Linq;
using ThinkAnimal.Model;

namespace ThinkAnimal.Repository
{
    public class AnimalsRepository
    {
        /// <summary>
        /// Get all animals from data base
        /// </summary>
        /// <returns></returns>
        public List<Animal> GetAnimals()
        {
            using (var projectContext = new ProjectContext())
            {
                return projectContext.Animals != null ? projectContext.Animals.ToList() : null;
            }
        }
    }
}