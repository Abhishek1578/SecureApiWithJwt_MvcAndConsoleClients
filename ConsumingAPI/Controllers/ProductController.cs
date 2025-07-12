using ConsumingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConsumingAPI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory httpclient;
        public ProductController(IHttpClientFactory httpClient)
        {
            this.httpclient = httpClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        { 
            var client  = GetClientWithToken();
            var response =await client.GetAsync("https://localhost:7089/api/Product");
            if (response.IsSuccessStatusCode)
            {
                var data =await response.Content.ReadFromJsonAsync<List<Product>>();
                return View(data);
            }
            ModelState.AddModelError("", "Something went wrong");
            return View(new List<Product>());
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var client = GetClientWithToken();
            var response =await client.PostAsJsonAsync("https://localhost:7089/api/Product", product);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something went wrong");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = GetClientWithToken();
            var response =await client.GetAsync($"https://localhost:7089/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data =await response.Content.ReadFromJsonAsync<Product>();
                return View(data);
            }
            ModelState.AddModelError("", "Something went Wrong ");
            //return View();
            return NotFound("Id is not found");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            var client = GetClientWithToken();
            var response =await client.PutAsJsonAsync("https://localhost:7089/api/Product", product);
            if(response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadFromJsonAsync<Product>();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something wen  wrong during Update Product");
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = GetClientWithToken();
            var response =await client.DeleteAsync($"https://localhost:7089/{id}");
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something went wrong during Delete Product");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var client = GetClientWithToken();
            var response =await client.GetAsync($"https://localhost:7089/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<Product>();
                return View(data);
            }
            return NotFound("Id is not found ");
        }
        private HttpClient GetClientWithToken()
        {
            var client = httpclient.CreateClient();
            var token = HttpContext.Session.GetString("jwttoken");
            Console.Write(token);
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }

    }
}
