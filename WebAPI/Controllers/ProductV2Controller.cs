using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v2/Product")]
    [ApiController]
    public class ProductV2Controller : ControllerBase
    {
        AppDbContext _db;
        public ProductV2Controller(AppDbContext db)
        {
            _db = db;
        }
        //GET: api/v2/Product
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _db.Products.ToList();
        }
    }
}
