using EcommerceWebSiteAPIs.Models;
using EcommerceWebSiteAPIs.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EcommerceWebSiteAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class OrderController : ControllerBase
    {
        IOrderRepository OrderRepository;
        UserManager<ApplicationUser> Usermanager;
       
        //CRDU get-post-put-delete
        public OrderController(IOrderRepository _Orderrepository
            ,UserManager<ApplicationUser> _Usermanager
            )
        {
            OrderRepository = _Orderrepository;
            Usermanager = _Usermanager;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            List<Order> Orders = new List<Order>();
                Orders = OrderRepository.GetAll();
            if (Orders == null)
            {
                return BadRequest("Empty Orders");
            }
            return Ok(Orders);
        }


        [HttpGet("UserOrders")]
        public IActionResult getAllByUserID()
        {
            List<Order> Orders = new List<Order>();
            var currentuser = Usermanager.Users.FirstOrDefault(user => user.UserName == User.Identity.Name); ;
            if(currentuser != null)
            {
                Orders=OrderRepository.GetAllOrderByUserID(currentuser.Id);
            }
            if (Orders == null)
            {
                return BadRequest("Empty Orders");
            }
            return Ok(Orders);
        }



        [HttpGet("{id:int}", Name = "getOneOrderRoute")]
        //[Route("{id}")]
        public IActionResult getByID(int id)
        {
            if (OrderRepository.GetById(id) == null)
            {
                return BadRequest("Empty Order");
            }
            return Ok(OrderRepository.GetById(id));
        }

        [HttpPost("CreateOrder")]
        public IActionResult Create(double TotalPrice)
        {
            
            if (ModelState.IsValid)
            {
                var user =Usermanager.Users.FirstOrDefault(user => user.UserName == User.Identity.Name); ;
                Order order = new Order();
                if (user != null)
                {
                    order.CusomerId = user.Id;
                }
                else
                {
                    order.CusomerId = "hellloooOrder";
                }
                order.TotalPrice = TotalPrice;
                try
                {
                    order.Id = OrderRepository.Insert(order.CusomerId,TotalPrice);
                    string url = Url.Link("getOneRoute", new { id = order.Id });
                    return Created(url, order);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }




        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    order.Id = OrderRepository.Edit(id, order);

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
                OrderRepository.Delete(id);

                return StatusCode(204, "Data Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
