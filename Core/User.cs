
using System.ComponentModel.DataAnnotations;


namespace Core;

public class User
{
    public int Id { get; set; }
        
    [Required(ErrorMessage = "Brugernavn er påkrævet")]
    public string Username { get; set; } 
        
    [Required(ErrorMessage = "Adgangskode er påkrævet")]
    [StringLength(10, MinimumLength = 8, ErrorMessage = "Adgangskoden skal være mellem 8 og 10 tegn")]
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