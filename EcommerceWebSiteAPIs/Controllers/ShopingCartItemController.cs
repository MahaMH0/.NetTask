using EcommerceWebSiteAPIs.Models;
using EcommerceWebSiteAPIs.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EcommerceWebSiteAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ShopingCartItemController : ControllerBase
    {
        IShopingCartItemRepository ShopingCartItemRepository;
        //CRDU get-post-put-delete
        public ShopingCartItemController(IShopingCartItemRepository _ShopingCartItemrepository)
        {
            ShopingCartItemRepository = _ShopingCartItemrepository;
        }
        [HttpGet]
        public IActionResult getAll(int OrderID=0)
        {
            List<ShopingCartItem> ShopingCartItems = new List<ShopingCartItem>();
            if (OrderID > 0)
            {
                ShopingCartItems = ShopingCartItemRepository.
                    GetShopingCartItemssByOrderId(OrderID);
            }
            else
            {
                ShopingCartItems = ShopingCartItemRepository.GetAll();
            }
            if (ShopingCartItems == null)
            {
                return BadRequest("Empty ShopingCartItems");
            }
            return Ok(ShopingCartItems);
        }

        [HttpGet("{id:int}", Name = "getOneShopingCartItemRoute")]
        //[Route("{id}")]
        public IActionResult getByID(int id)
        {
            if (ShopingCartItemRepository.GetById(id) == null)
            {
                return BadRequest("Empty ShopingCartItem");
            }
            return Ok(ShopingCartItemRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(ShopingCartItem ShopingCartItem)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    ShopingCartItem.Id = ShopingCartItemRepository.Insert(ShopingCartItem);
                    string url = Url.Link("getOneRoute", new { id = ShopingCartItem.Id });
                    return Created(url, ShopingCartItem);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }




        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, ShopingCartItem ShopingCartItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ShopingCartItem.Id = ShopingCartItemRepository.Edit(id, ShopingCartItem);

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
        public IActionResult Delete(int id)
        {
            try
            {
                ShopingCartItemRepository.Delete(id);

                return StatusCode(204, "Data Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertCart")]
        public IActionResult InsertCart(List<ShopingCartItem> shopingCartItems)
        {
            try
            {
                foreach (ShopingCartItem item in shopingCartItems)
                {
                    ShopingCartItemRepository.Insert(item);
                }

                return StatusCode(204, "CartUpdated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
