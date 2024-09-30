using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CampusCuisine.Entity
{

    [Table("recipe")]
    [PrimaryKey(nameof(UserId), nameof(Id))]
    public class RecipeEntity(Guid userId, Guid id, string name, string category, string ingredients, string instructions)
    {
        public Guid UserId { get; set; } = userId;
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Category { get; set; } = category;
        public string Ingredients { get; set; } = ingredients;
        public string Instructions { get; set; } = instructions;
    }
}