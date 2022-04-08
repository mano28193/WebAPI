using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        HttpClient client;
        Uri baseAddress;
        IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            baseAddress = new Uri(_configuration["APIAddress"]);

            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            IEnumerable<ProductModel> model = new List<ProductModel>();
            var response = client.GetAsync(client.BaseAddress+"/product").Result;
            if(response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(data);
            }
            return View(model);
        }

        IEnumerable<CategoryModel> GetCategories()
        {
            IEnumerable<CategoryModel> model = new List<CategoryModel>();
            var response = client.GetAsync(client.BaseAddress + "/Category").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonSerializer.Deserialize<IEnumerable<CategoryModel>>(data);
            }
            return model;
        }
        
        public IActionResult Create()
        {
            ViewBag.CategoryList = GetCategories();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            string strData = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(strData,Encoding.UTF8,"application/json");
            var response = client.PostAsync(client.BaseAddress+"/product", content).Result;
            if (response.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = GetCategories();
            return View();
        }
       
        public IActionResult Delete(int id)
        {
            var response = client.DeleteAsync(client.BaseAddress + "/product/" + id).Result;
            if (response.IsSuccessStatusCode) 
            {
                return RedirectToAction("Index"); 
            }

            return RedirectToAction("Index");
        }
    }
}
