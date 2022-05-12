using EcommerceWebSiteAPIs.Models;
using System.Collections.Generic;

namespace EcommerceWebSiteAPIs.Repository
{
    public interface IProductRepository
    {
        int Delete(int id);
        int Edit(int id, Product newProduct);
        List<Product> GetAll();
        Product GetById(int id);
        int Insert(Product Product);
        List<Product> GetProductsByCategoryId(int categoryID);
    }
}