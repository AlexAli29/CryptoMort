namespace CryptoStockMVC.Models
{
    public class CurrencyQuote
    {
        public decimal Price { get; set; }
        public decimal Percent_change_24h { get; set; }
        public decimal Market_cap { get; set; }
    }
}
