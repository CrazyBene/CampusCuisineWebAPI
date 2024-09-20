namespace CampusCuisine.Model
{
    public class Rating
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int? Value { get; set; }
        public string? Comment { get; set; }
    }
}