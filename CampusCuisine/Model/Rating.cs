namespace CampusCuisine.Model
{
    public class Rating
    {
        public Guid? Id { get; set; }
        public Guid? RecipeId { get; set; }
        public int? Value { get; set; }
        public string? Comment { get; set; }
    }
}