using System.ComponentModel.DataAnnotations;

namespace ShoppingListServer.Models
{
    public class ShoppingItem
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime? CompletedAt { get; set; }
    }
}
