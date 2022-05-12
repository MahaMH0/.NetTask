using EcommerceWebSiteAPIs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceWebSiteAPIs.Repository
{
    public class ShopingCartItemRepository : IShopingCartItemRepository
    {
        EcommerceWebSiteEntities Ecommerce;
        public ShopingCartItemRepository(EcommerceWebSiteEntities _Ecommerce)
        {
            Ecommerce = _Ecommerce;
        }
        public List<ShopingCartItem> GetAll()
        {
            return Ecommerce.ShoppingCartItems.ToList();
        }
        public ShopingCartItem GetById(int id)
        {
            return Ecommerce.ShoppingCartItems.Include(ShopingCartItem=>ShopingCartItem.Order).FirstOrDefault(ShopingCartItem => ShopingCartItem.Id == id);
        }
        public int Insert(ShopingCartItem ShopingCartItem)
        {
            ShopingCartItem newShopingCartItem = ShopingCartItem;
            Ecommerce.ShoppingCartItems.Add(newShopingCartItem);
            Ecommerce.SaveChanges();
            return newShopingCartItem.Id;
        }
        public int Edit(int id, ShopingCartItem newShopingCartItem)
        {
            ShopingCartItem oldShopingCartItem = Ecommerce.ShoppingCartItems.FirstOrDefault(ShopingCartItem => ShopingCartItem.Id == id);
            oldShopingCartItem.ProductName = newShopingCartItem.ProductName;
            oldShopingCartItem.ItemPrice = newShopingCartItem.ItemPrice;
            oldShopingCartItem.SelectedQuantity=newShopingCartItem.SelectedQuantity;
            Ecommerce.Entry(oldShopingCartItem).State = EntityState.Modified;
            Ecommerce.SaveChanges();
            return oldShopingCartItem.Id;
        }
        public int Delete(int id)
        {
            ShopingCartItem ShopingCartItem = Ecommerce.ShoppingCartItems.FirstOrDefault(ShopingCartItem => ShopingCartItem.Id == id);
            Ecommerce.ShoppingCartItems.Remove(ShopingCartItem);
            Ecommerce.SaveChanges();
            return id;
        }

        public List<ShopingCartItem> GetShopingCartItemssByOrderId(int OrderID)
        {
            List<ShopingCartItem> CartItems = new List<ShopingCartItem>();
            CartItems = Ecommerce.ShoppingCartItems.Where(cartitem => cartitem.OrderId == OrderID).ToList();
            return CartItems;
        }

    }
}
