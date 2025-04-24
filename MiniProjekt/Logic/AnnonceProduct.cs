using System.ComponentModel.DataAnnotations;
namespace MiniProjekt.Logic;

public class AnnonceProduct
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
    public string? Name { get; set; }

    [Required]
    [Range(1,int.MaxValue, ErrorMessage = "Price must be positive")]
    public int? Price { get; set; }

    [Required]
    public string? Description { get; set; }
        
    public DateTime PublishedDate { get; set; } = DateTime.Now;

    public bool IsPublished { get; set; }

    [Required]
    [Range(0,int.MaxValue, ErrorMessage = "Stock must be positive")]
    public int Stock { get; set; }
    
    public string Category { get; set; }

}