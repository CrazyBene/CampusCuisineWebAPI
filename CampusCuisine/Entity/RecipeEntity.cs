using System.ComponentModel.DataAnnotations.Schema;

namespace CampusCuisine.Entity
{

    [Table("recipe")]
    public class RecipeEntity(Guid id, string name, string category, string ingredients, string instructions)
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Category { get; set; } = category;
        public string Ingredients { get; set; } = ingredients;
        public string Instructions { get; set; } = instructions;
    }
}