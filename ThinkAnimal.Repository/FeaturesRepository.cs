using System.Collections.Generic;
using System.Linq;
using ThinkAnimal.Model;

namespace ThinkAnimal.Repository
{
    public class FeaturesRepository
    {
        /// <summary>
        /// Get next feature after player answer
        /// </summary>
        /// <param name="currentFeature"></param>
        /// <returns></returns>
        public Feature GetNextFeature(Feature currentFeature)
        {
            using (var projectContext = new ProjectContext())
            {
                return projectContext.
                    Features.
                    Include("Animal").
                    SingleOrDefault(f => f.ParentFeature.Id == currentFeature.Id && 
                        f.ParentFeatureAnswerIsYes == currentFeature.IsYes);
            }
        }
        
        /// <summary>
        /// Get first feature in game begining
        /// </summary>
        /// <returns></returns>
        public Feature GetFeature()
        {
            using (var projectContext = new ProjectContext())
            {
                return projectContext.
                    Features.
                    Include("Animal").
                    SingleOrDefault( f => f.ParentFeature == null);
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
                    ToList();

                foreach (var feature in features)
                {
                    feature.ChildFeatureForYes = GetChildFeatureByAnswer(feature, true);
                    feature.ChildFeatureForNo = GetChildFeatureByAnswer(feature, false);
                }

                return features;
            }
        }

        /// <summary>
        /// Get child feature for after "Yes" or "No" player answers
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="isYes"></param>
        /// <returns></returns>
        public Feature GetChildFeatureByAnswer(Feature feature, bool isYes)
        {
            using (var projectContext = new ProjectContext())
            {
                return projectContext.
                        Features.
                        Include("Animal").
                        SingleOrDefault(f => f.ParentFeature.Id == feature.Id && f.ParentFeatureAnswerIsYes == isYes);

            }

        }

        /// <summary>
        /// Save feature and animal to database
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public bool SaveFeature(Feature feature)
        {
            using (var context = new ProjectContext())
            {
                Feature parentFeature = context.Features.SingleOrDefault(f => f.Id == feature.ParentFeatureId);
                feature.ParentFeature = parentFeature;

                context.Features.Add(feature);
                context.SaveChanges();
                return true;
            }
        }
    }
}