using System.ComponentModel.DataAnnotations;

namespace CampusCuisine.Model
{
    public class Recipe
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "The {0} value needs to be between {2} and {1} characters inclusive.")]
        public string? Name { get; set; }

        [Required]
        public string? Category { get; set; }

        [StringLength(200)]
        public string? Ingredients { get; set; }

        [StringLength(200)]
        public string? Instructions { get; set; }
    }
}