using System.ComponentModel.DataAnnotations.Schema;

namespace CampusCuisine.Entity
{

    [Table("rating")]
    public class RatingEntity(Guid userId, Guid id, Guid recipeId, int value, string? comment)
    {
        public Guid UserId { get; set; } = userId;
        public Guid Id { get; set; } = id;
        public Guid RecipeId { get; set; } = recipeId;
        public int Value { get; set; } = value;
        public string? Comment { get; set; } = comment;
    }
}