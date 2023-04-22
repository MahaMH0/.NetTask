using BackendPart.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendPart.Repository
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
    }
}
