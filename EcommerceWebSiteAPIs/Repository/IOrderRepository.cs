using EcommerceWebSiteAPIs.Models;
using System;
using System.Collections.Generic;

namespace EcommerceWebSiteAPIs.Repository
{
    public interface IOrderRepository
    {
        int Delete(int id);
        int Edit(int id, Order newOrder);
        List<Order> GetAll();
        Order GetById(int id);
        int Insert(string userid,double totalprice);
        List<Order> GetAllOrderByUserID(string userid);
    }
}