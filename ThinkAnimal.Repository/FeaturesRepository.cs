using System.Collections.Generic;
using System.Linq;
using ThinkAnimal.Model;

namespace ThinkAnimal.Repository
{
    public interface IFeaturesRepository
    {
        Feature GetFirstFeature();
        List<Feature> GetAllFeatures();
        Feature GetFeatureById(int id);
        bool AddFeature(Feature feature);
        bool DeleteFeatureAndAnimal(int id);
        bool SaveFeatureChanges(Feature updatedFeature);
    }

    public class FeaturesRepository : IFeaturesRepository
    {    
        /// <summary>
        /// Get first feature in game begining
        /// </summary>
        /// <returns></returns>
        public Feature GetFirstFeature()
        {
            using (var projectContext = new ProjectContext())
            {
                return projectContext.
                    Features.
                    Include("Animal").
                    Include("ChildFeatureForYes").
                    Include("ChildFeatureForNo").
                    OrderBy(f => f.Id).
                    FirstOrDefault();
            }
        }

        /// <summary>
        /// Get all features to manage
        /// </summary>
        /// <returns></returns>
        public List<Feature> GetAllFeatures()
        {
            using (var projectContext = new ProjectContext())
            {
                List<Feature> features = projectContext.
                    Features.
                    Include("Animal").
                    Include("ChildFeatureForYes").
                    Include("ChildFeatureForNo").
                    ToList();

                return features;
            }
        }

        /// <summary>
        /// Get feature with child elements
        /// </summary>
        /// <returns></returns>
        public Feature GetFeatureById(int id)
        {
            using (var projectContext = new ProjectContext())
            {
                return projectContext.
                    Features.
                    Include("Animal").
                    Include("ChildFeatureForYes").
                    Include("ChildFeatureForNo").
                    SingleOrDefault(f => f.Id == id);
            }
        }

        /// <summary>
        /// Save feature and animal to database
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public bool AddFeature(Feature feature)
        {
            using (var context = new ProjectContext())
            {
                //Save feature
                context.Features.Add(feature);
                Feature parentFeature = context.Features.SingleOrDefault(f => f.Id == feature.ParentFeatureId);

                //Set new feature as child feature for parent 
                if (parentFeature != null)
                {
                    if (feature.IsYes)
                        parentFeature.ChildFeatureForYes = feature;
                    else
                        parentFeature.ChildFeatureForNo = feature;
                }

                context.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Delete feature and animal from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteFeatureAndAnimal(int id)
        {
            using (var context = new ProjectContext())
            {
                RemoveFeatureFromParent(id, context);
                Feature feature = context.
                                    Features.
                                    Include("Animal").
                                    SingleOrDefault(f => f.Id == id);
                if (feature != null)
                {
                    //Delete feature and animal
                    context.Animals.Remove(feature.Animal);
                    context.Features.Remove(feature);
                }
                context.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Before delete we must remove feature from parent feature
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        private void RemoveFeatureFromParent(int id, ProjectContext context)
        {
            //feature will be ChildFeatureForYes or ChildFeatureForNo shild
            Feature parentFeature = context.
                Features.
                Include("Animal").
                SingleOrDefault(f => f.ChildFeatureForYes.Id == id);
            if (parentFeature == null)
            {
                parentFeature = context.
                    Features.
                    Include("Animal").
                    SingleOrDefault(f => f.ChildFeatureForNo.Id == id);
                if (parentFeature != null) parentFeature.ChildFeatureForNo = null;
            }
            else
            {
                parentFeature.ChildFeatureForYes = null;
            }
        }

        /// <summary>
        /// Save changed feature and animal to db
        /// </summary>
        /// <param name="updatedFeature"></param>
        public bool SaveFeatureChanges(Feature updatedFeature)
        {
            using (var context = new ProjectContext())
            {
                Feature feature =
                    context.
                    Features.
                    Include("Animal").
                    SingleOrDefault(f => f.Id == updatedFeature.Id);
                
                if (feature == null) return false;
                
                //Save updated question properties to db
                feature.Text = updatedFeature.Text;

                if (feature.Animal != null)
                    feature.Animal.Title = updatedFeature.Animal.Title;

                context.SaveChanges();
                return true;
            }
        }

    }
}