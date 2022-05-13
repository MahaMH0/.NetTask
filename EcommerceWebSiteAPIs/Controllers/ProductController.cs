using EcommerceWebSiteAPIs.Models;
using EcommerceWebSiteAPIs.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EcommerceWebSiteAPIs.Controllers
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
        public IActionResult getAll(int CategoryID = 0)
        {
            List<Product> Products = new List<Product>();
            if (CategoryID > 0)
            {
                Products = productRepository.
                GetProductsByCategoryId(CategoryID);

            }
            else
            {
                Products = productRepository.GetAll();
            }
            if (Products == null)
            {
                return BadRequest("Empty Products");
            }
            return Ok(Products);
        }
        [HttpGet("{id:int}", Name = "getOneRoute")]
        //[Route("{id}")]
        public IActionResult getByID(int id)
        {
            if (productRepository.GetById(id) == null)
            {
                return BadRequest("Empty Product");
            }
            return Ok(productRepository.GetById(id));
        }
        [HttpPost]
        [Authorize("Admin")]
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

                    return StatusCode(204, "Data Saved");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{id:int}")]
        [Authorize("Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                productRepository.Delete(id);

                return StatusCode(204, "Data Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
