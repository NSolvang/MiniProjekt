using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core;

public class User
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class Seller : User
{
     public int SellerID { get; set; }
     
     public string Name { get; set; } 
    
}

public class Buyer : User
{
    public int BuyerID { get; set; }
    
    public string Name { get; set; }
}

