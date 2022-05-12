using EcommerceWebSiteAPIs.Models;
using System;
using System.Collections.Generic;

namespace EcommerceWebSiteAPIs.Repository
{
    public interface IShopingCartItemRepository
    {
        int Delete(int id);
        int Edit(int id, ShopingCartItem newShopingCartItem);
        List<ShopingCartItem> GetAll();
        ShopingCartItem GetById(int id);
        int Insert(ShopingCartItem newShopingCartItem);
        List<ShopingCartItem> GetShopingCartItemssByOrderId(int OrderID);
    }
}