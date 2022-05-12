using EcommerceWebSiteAPIs.Models;
using System.Collections.Generic;

namespace EcommerceWebSiteAPIs.Repository
{
    public interface ICategoryRepository
    {
        int Delete(int id);
        int Edit(int id, Category newCategory);
        List<Category> GetAll();
        Category GetById(int id);
        int Insert(Category category);
    }
}