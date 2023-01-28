using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restaurant.data.Abstract;
using restaurant.entity;

namespace restaurant.data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category>, ICategoryRepository
    {
        public EfCoreCategoryRepository(ShopContext context) : base(context)
        {
            
        }
         private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }   
        public void DeleteFromCategory(int foodId, int categoryId)
        {
             
                var cmd = "delete from foodcategories where FoodId=@p0 and CategoryId=@p1";
                ShopContext.Database.ExecuteSqlRaw(cmd,foodId,categoryId);
           
        }

        public Category GetByIdWithFoods(int categoryId)
        {
            
                return ShopContext.Categories
                                .Where(i=>i.CategoryId == categoryId)
                                .Include(c=>c.FoodCategories)
                                .ThenInclude(p=>p.Food)
                                .FirstOrDefault();
        
        }
    }
}