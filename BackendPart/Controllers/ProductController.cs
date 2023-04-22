using BackendPart.Models;
using BackendPart.Repository;
using Microsoft.AspNetCore.Mvc;


namespace BackendPart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ProductController : ControllerBase
    {
        IProductRepository productRepository;
        //CRDU get-post-put-delete
        public ProductController(IProductRepository _productrepository)
        {
            productRepository = _productrepository;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            List<Product> Products = new List<Product>();

            Products = productRepository.GetAll();
            if (Products == null)
            {
                return BadRequest("No Products Found");
            }
            return Ok(Products);
        }
        [HttpGet("{id:int}", Name = "getOneRoute")]
        //[Route("{id}")]
        public IActionResult getByID(int id)
        {
            if (productRepository.GetById(id) == null)
            {
                return BadRequest("Product Not Found");
            }
            return Ok(productRepository.GetById(id));
        }
        [HttpPost]
        //[Authorize("Admin")]
        public IActionResult Create(Product product)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    product.id = productRepository.Insert(product);
                    string url = Url.Link("getOneRoute", new { id = product.id });
                    return Created(url, product);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }

        [HttpPut("{id:int}")]
        //[Authorize("Admin")]
        public IActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    product.id = productRepository.Edit(id, product);

                    return StatusCode(204, "Product Saved");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id:int}")]
        //[Authorize("Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                productRepository.Delete(id);

                return StatusCode(204, "Product Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
