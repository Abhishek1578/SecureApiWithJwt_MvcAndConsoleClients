
using System.Net.Http.Json;

namespace APIConsumeInConsoleApplication
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

    }
    class Program
    {
        static async Task Main(string[] args)
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTc1MjI1Mzc2NywiaXNzIjoiVXNlciIsImF1ZCI6IkFiaGlzaGVrIn0.Ex4P-YnwaOgIGjiIxK0GnOBMbA7gao1lwiAPqMDv2xo";
            var client=new HttpClient();
            client.BaseAddress= new Uri("https://localhost:7089/");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            try
            {
                var response =await client.GetAsync("api/Product");
                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<List<Product>>();
                    foreach(var p in products)
                    {
                        Console.WriteLine($"{p.Id}: {p.Name } | {p.Description} - ₹{p.Price}");
                        Console.ReadKey();
                    }
                   
                }
                else
                {
                    Console.WriteLine("Something Went Wrong ");
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}