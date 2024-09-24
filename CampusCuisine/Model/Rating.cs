using System.ComponentModel.DataAnnotations;

namespace CampusCuisine.Model
{
    public class Rating
    {
        public Guid? Id { get; set; }

        public Guid? RecipeId { get; set; }

        [Required]
        [Range(0, 10)]
        public int? Value { get; set; }

        [StringLength(100)]
        public string? Comment { get; set; }
    }
}