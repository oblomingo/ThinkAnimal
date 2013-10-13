using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThinkAnimal.Model
{
    public class Feature
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Empty feature text")]
        [StringLength(200, ErrorMessage = "Feature text can't be longer than 200 simbols")]
        public string Text { get; set; }
        
        //Animal object to this feature
        public Animal Animal { get; set; }
        
        //Child feature for next question if player choose "Yes" for current question
        public Feature ChildFeatureForYes { get; set; }

        //Child feature for next question if player choose "No" for current question
        public Feature ChildFeatureForNo { get; set; }

        //Properties for temporary data, not for database
        [NotMapped]
        public bool IsYes { get; set; }
        [NotMapped]
        public int ParentFeatureId { get; set; }
    }
}
