using EcommerceWebSiteAPIs.DTO;
using EcommerceWebSiteAPIs.Models;
using EcommerceWebSiteAPIs.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EcommerceWebSiteAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoryController : ControllerBase
    {

        ICategoryRepository CategoryRepository;
        //CRDU get-post-put-delete
        public CategoryController(ICategoryRepository _Categoryrepository
            )
        {
            CategoryRepository = _Categoryrepository;
        }
        [HttpGet]
        public IActionResult getAll()
        {

            if (CategoryRepository.GetAll() == null)
            {
                return BadRequest("Empty Categorys");
            }
            return Ok(CategoryRepository.GetAll());
        }
        [HttpGet("{id:int}", Name = "getOneCategoryRoute")]
        //[Route("{id}")]
        public IActionResult getByID(int id)
        {
            if (CategoryRepository.GetById(id) == null)
            {
                return BadRequest("Empty Category");
            }
            return Ok(CategoryRepository.GetById(id));
        }
        [HttpPost]
        [Authorize("Admin")]
        public IActionResult Create(Category Category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Category.id= CategoryRepository.Insert(Category);
                    string url = Url.Link("getOneRoute", new { id = Category.id });
                    return Created(url, Category);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
        [HttpPatch("{id:int}")]
        [Authorize("Admin")]
        public IActionResult Edit(int id, Category Category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Category.id = CategoryRepository.Edit(id, Category);

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
                CategoryRepository.Delete(id);

                return StatusCode(204, "Data Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
