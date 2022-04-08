using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        AppDbContext _db;
        public ProductController(AppDbContext db)
        {
            _db = db;
        }
        //GET: api/Product
        [HttpGet]
        public IEnumerable<Product> GetAll(int version = 1)
        {
            if(version > 1)
                return _db.Products.ToList();
            else
                return _db.Products.ToList();
        }
        //GET: api/Product/{id}  --> Therse are URI -  (Verb+URL)
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _db.Products.Find(id);
        }
        [HttpPost]
        public IActionResult Add(Product model)
        {
            try
            {
                _db.Products.Add(model);
                _db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created,model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, null);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product model)
        {
            try
            {
                if (id != model.ProductId)
                    return BadRequest();
                _db.Products.Update(model);
                _db.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPatch("{id}")]
        public IActionResult Modify(int id, Product model)
        {
            try
            {
                Product product = _db.Products.Find(id);
                product.Description = model.Description;
                product.UnitPrice = model.UnitPrice;
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
       // [Route("api/product/delete/id")]
        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            try
            {
                Product model = _db.Products.Find(id);
                if(model != null) 
                { 
                    _db.Products.Remove(model);
                    _db.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,null);
            }
        }
    }
}
