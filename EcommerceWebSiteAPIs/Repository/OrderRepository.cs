using EcommerceWebSiteAPIs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceWebSiteAPIs.Repository
{
    public class OrderRepository : IOrderRepository
    {
        EcommerceWebSiteEntities Ecommerce;
        public OrderRepository(EcommerceWebSiteEntities _Ecommerce)
        {
            Ecommerce = _Ecommerce;
        }
        public List<Order> GetAll()
        {
            return Ecommerce.Order.Include(order=>order.ShopingCartItems).ToList();
        }
        public Order GetById(int id)
        {
            return Ecommerce.Order.Include(order=>order.ShopingCartItems).FirstOrDefault(Order => Order.Id == id);
        }

        public List<Order> GetAllOrderByUserID(string userid)
        {
            List<Order> orders=Ecommerce.Order.Where(order=>order.CusomerId == userid).Include(order=>order.ShopingCartItems).ToList();
            return orders;
        }
        public int Insert(string userid,double totalprice)
        {
            Order neworder = new Order();
            neworder.CusomerId = userid;
            neworder.TotalPrice = totalprice;
            Ecommerce.Order.Add(neworder);
            Ecommerce.SaveChanges();
            return neworder.Id;
        }
        public int Edit(int id, Order newOrder)
        {
            Order oldOrder = Ecommerce.Order.FirstOrDefault(Order => Order.Id == id);
            oldOrder.CreatedDate = DateTime.Now;
            oldOrder.TotalPrice=newOrder.TotalPrice;
            oldOrder.ShopingCartItems = newOrder.ShopingCartItems;
            Ecommerce.Entry(oldOrder).State = EntityState.Modified;
            Ecommerce.SaveChanges();
            return oldOrder.Id;
        }
        public int Delete(int id)
        {
            Order Order = Ecommerce.Order.FirstOrDefault(Order => Order.Id == id);
            Ecommerce.Order.Remove(Order);
            Ecommerce.SaveChanges();
            return id;
        }


    }
}
