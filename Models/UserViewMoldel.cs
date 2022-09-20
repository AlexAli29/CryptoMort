using Microsoft.AspNetCore.Identity;
namespace CryptoStockMVC.Models
{
    public class UserViewMoldel : IdentityUser
    {
        public string? ImagePath { get; set; }

        public IFormFile? Image { get; set; }
    }
}
