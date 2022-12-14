using System.ComponentModel.DataAnnotations;

namespace CryptoStockMVC.Models
{
    public class Coin
    {
        
        public int Id { get; set; }        
        public string Name { get; set; }       

        public Dictionary<string, CurrencyQuote> Quote { get; set; }
        public string Symbol { get; set; }  

      
    }
}
