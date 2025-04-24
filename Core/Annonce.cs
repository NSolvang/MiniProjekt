using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace Core;

public class Annonce
{
    
    public ObjectId _id { get; set; }
    public int AnnonceID { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 50 characters.")]
    public string Title { get; set; }
    
    [Required]
    [Range(1,int.MaxValue, ErrorMessage = "Price must be positive")]
    public int Price { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    public string Category { get; set; }
    
    public string Status { get; set; }

}
