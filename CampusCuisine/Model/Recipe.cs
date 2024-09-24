namespace CampusCuisine.Model
{
    public class Recipe
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Ingredients { get; set; }
        public string? Instructions { get; set; }
    }
}