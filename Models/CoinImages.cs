namespace CryptoStockMVC.Models
{
    public class CoinImages
    {
        private Dictionary<string, string> _pathes;

        public CoinImages()
        {
            _pathes = new Dictionary<string, string>()
            {
                {"BTC","/images/coins-images/bitcoin.png" },
                {"ETH","/images/coins-images/etherium.png" }
            };
        }

        public string GetPath(string symbol)
        {
            _pathes.TryGetValue(symbol, out string value);
            return value ?? string.Empty;
        }
    }

    
}
