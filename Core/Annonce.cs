using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace Core;

public class Annonce
{
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    public string? Id { get; set; }   
    
    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 50 characters.")]
    public string Title { get; set; }
    
    [Required]
    [Range(1,int.MaxValue, ErrorMessage = "Price must be positive")]
    public int Price { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    public string Category { get; set; }

    public string Status { get; set; } = "Aktiv";
    
    [Required]
    public int SellerId { get; set; } // Referencer Seller (User)

    // NYT: Reference til Buyer (hvis annoncen er solgt)
    public int? BuyerId { get; set; } // Nullable fordi det måske ikke er solgt endnu
    
    public DateTime? SoldDate { get; set; }
    
    public string Locations { get; set; }

}
