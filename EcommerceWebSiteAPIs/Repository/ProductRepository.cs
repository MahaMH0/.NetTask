using EcommerceWebSiteAPIs.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceWebSiteAPIs.Repository
{
    public class ProductRepository : IProductRepository
    {
        EcommerceWebSiteEntities Ecommerce;
        public ProductRepository(EcommerceWebSiteEntities _Ecommerce)
        {
            Ecommerce = _Ecommerce;
        }
        public List<Product> GetAll()
        {
            return Ecommerce.Product.ToList();
        }
        public Product GetById(int id)
        {
            return Ecommerce.Product.FirstOrDefault(Product => Product.id == id);
        }
        public int Insert(Product Product)
        {
            Product newProduct = Product;
            Ecommerce.Product.Add(newProduct);
            Ecommerce.SaveChanges();
            return newProduct.id;

        }
        public int Edit(int id, Product newProduct)
        {
            Product oldProduct = Ecommerce.Product.FirstOrDefault(Product => Product.id == id);
            oldProduct.Name = newProduct.Name;
            oldProduct.Price = newProduct.Price;
            oldProduct.CateogryID = newProduct.CateogryID;
            oldProduct.Quantity = newProduct.Quantity;
            oldProduct.Img = newProduct.Img as string;

            Ecommerce.Entry(oldProduct).State = EntityState.Modified;
            Ecommerce.SaveChanges();
            return oldProduct.id;
        }
        public int Delete(int id)
        {
            Product Product = Ecommerce.Product.FirstOrDefault(Product => Product.id == id);
            Ecommerce.Product.Remove(Product);
            Ecommerce.SaveChanges();
            return id;
        }
        public List<Product> GetProductsByCategoryId(int CategoryID)
        {
            List<Product> products = new List<Product>();
            products = Ecommerce.Product.Where(product=>product.CateogryID == CategoryID).ToList();
            return products;
        }
    }
}
