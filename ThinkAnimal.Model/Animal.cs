using System.ComponentModel.DataAnnotations;

namespace ThinkAnimal.Model
{
    public class Animal
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Empty animal title")]
        [StringLength(100, ErrorMessage = "Animale title can't be longer than 100 simbols")]
        public string Title { get; set; }
    }
}
