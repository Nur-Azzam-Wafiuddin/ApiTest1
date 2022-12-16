using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiTest1.Model;

namespace ApiTest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext dataContext;

        public ProductController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            return Ok(await dataContext.Products.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> GetbyId(int id)
        {
            var product = await dataContext.Products.FindAsync(id);
            if(product == null)
            {
                return BadRequest("Product not found");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(Product product)
        {
            dataContext.Products.Add(product);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.Products.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Product>>> UpdateProductbyId(Product product, int id)
        {
            var updateProduct = await dataContext.Products.FindAsync(id);
            if (product == null)
            {
                return BadRequest("Product not found");
            }

            updateProduct.Name = product.Name;
            updateProduct.isAvailable = product.isAvailable;

            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Products.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> DeletebyId(int id)
        {
            var deleteProduct = await dataContext.Products.FindAsync(id);
            if (deleteProduct == null)
            {
                return BadRequest("Product not found");
            }
            dataContext.Products.Remove(deleteProduct);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Products.ToListAsync());
        }
    }
}
