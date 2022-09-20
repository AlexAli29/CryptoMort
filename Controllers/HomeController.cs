using CryptoStockMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CryptoStockMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string? search)        {
            
            IEnumerable<Coin> coinList;

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
                if (!string.IsNullOrWhiteSpace(search))
                {
                    coinList = result.Data.Where(x => x.Name.ToLower().Contains(search.ToLower()) || x.Symbol.ToLower().Contains(search.ToLower())).ToList();
                }
                else
                {
                    coinList = result.Data.ToList();
                }
               
                return View(coinList);
                
                
                
            }
        }       

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                var signInresult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInresult.Succeeded)
                {
                   
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> Login()
        {
                 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User user, string password)
        {
            //var user = new User
            //{
            //    UserName = username,
            //    Email = email,
            //};

            if (user.Image != null)
            {
                var currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\user-avatars");
                var fileName = Guid.NewGuid().ToString() + ".jpg";
                var destinationPath = Path.Combine(currentDirectory, fileName);
                using (var stream = System.IO.File.Create(destinationPath))
                {
                    await user.Image.CopyToAsync(stream);
                }

                user.ImagePath = @"/images/" + fileName;
            }

            //_db.Categories.Add(obj.CreateModel());
            var rusult = await _userManager.CreateAsync(user, password);

            if (rusult.Succeeded)
            {

                var signInresult = await _signInManager.PasswordSignInAsync(user, password, false, false);

                if (signInresult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Signup()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}