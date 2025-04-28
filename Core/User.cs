using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Core;

public class User
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
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

