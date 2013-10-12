using System.ComponentModel.DataAnnotations.Schema;

namespace ThinkAnimal.Model
{
    public class Feature
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Animal Animal { get; set; }
        public Feature ParentFeature { get; set; }
        public bool ParentFeatureAnswerIsYes { get; set; }

        [NotMapped]
        public bool IsYes { get; set; }
        [NotMapped]
        public int ParentFeatureId { get; set; }
        [NotMapped]
        public Feature ChildFeatureForYes { get; set; }
        [NotMapped]
        public Feature ChildFeatureForNo { get; set; }
    }
}
