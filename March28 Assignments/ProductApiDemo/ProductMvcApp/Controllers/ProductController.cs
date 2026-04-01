using Microsoft.AspNetCore.Mvc;
using ProductMvcApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace ProductMvcApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client;

        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7023/api/Product/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("");
            var data = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(data);
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _client.PostAsync("", content);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync(id.ToString());
            var data = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(data);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _client.PutAsync("", content);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _client.DeleteAsync(id.ToString());
            return RedirectToAction("Index");
        }
    }
}