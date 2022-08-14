using CryptoStockMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Data.Entity;

namespace CryptoStockMVC.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //IEnumerable<Coin> coinList;

            string API_KEY = "c5151001-d906-43b3-a27b-6e2090cda339";
            var URL = new UriBuilder(@"https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            using (var _client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, URL.ToString()))
            {
                request.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
                request.Headers.Add("Accepts", "application/json");
                var response = await _client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Response is not successful");
                }

                var result = await response.Content.ReadFromJsonAsync<CoinResponse>();
                
               
                return View(result.Data);
                
                
                
            }
        }
       
    }
}