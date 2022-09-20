using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoStockMVC.Models
{
    public class User : IdentityUser
    {
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

    }
}
