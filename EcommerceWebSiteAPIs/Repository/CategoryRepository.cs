using EcommerceWebSiteAPIs.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceWebSiteAPIs.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        EcommerceWebSiteEntities Ecommerce;
        public CategoryRepository(EcommerceWebSiteEntities _Ecommerce)
        {
            Ecommerce = _Ecommerce;
        }
        public List<Category> GetAll()
        {
            return Ecommerce.Category.ToList();
        }
        public Category GetById(int id)
        {
            return Ecommerce.Category.Include(c=>c.Products).FirstOrDefault(Category => Category.id == id);
        }
        public int Insert(Category category)
        {
            Category newcategory = category;
            Ecommerce.Category.Add(newcategory);
            Ecommerce.SaveChanges();
            return newcategory.id;

        }
        public int Edit(int id, Category newCategory)
        {
            Category oldcategory = Ecommerce.Category.FirstOrDefault(Category => Category.id == id);
            oldcategory.Name = newCategory.Name;
            Ecommerce.Entry(oldcategory).State = EntityState.Modified;
            Ecommerce.SaveChanges();
            return oldcategory.id;
        }
        public int Delete(int id)
        {
            Category category = Ecommerce.Category.FirstOrDefault(category => category.id == id);
            Ecommerce.Category.Remove(category);
            Ecommerce.SaveChanges();
            return id;
        }

    }
}
