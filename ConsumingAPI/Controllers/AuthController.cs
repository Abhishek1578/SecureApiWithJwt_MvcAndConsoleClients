using ConsumingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConsumingAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory httpclient;
        public AuthController(IHttpClientFactory  httpClient)
        {
            this.httpclient = httpClient;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var client = httpclient.CreateClient();
            var response =await client.PostAsJsonAsync("https://localhost:7089/api/Auth", loginModel);
            if (response.IsSuccessStatusCode)
            {
                var tokenObj =await response.Content.ReadFromJsonAsync<TokenModel>();
                if(tokenObj!=null && !string.IsNullOrEmpty(tokenObj.token))
                {
                    HttpContext.Session.SetString("jwttoken", tokenObj.token);
                    return RedirectToAction("Index", "Product");

                }
            }
            return View(loginModel);
        }
    }
}
