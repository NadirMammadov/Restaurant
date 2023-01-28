using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restaurant.data.Abstract;
using restaurant.entity;

namespace restaurant.data.Concrete.EfCore
{
    public class EfCoreFoodRepository : EfCoreGenericRepository<Food>, IFoodRepository
    {
        public EfCoreFoodRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }
        public Food GetByIdWithCategories(int foodId)
        {

            return ShopContext.Foods
                         .Where(i => i.FoodId == foodId)
                         .Include(i => i.FoodCategories)
                         .ThenInclude(i => i.Category)
                         .FirstOrDefault();

        }

        public Food GetByName(string foodName)
        {

            return ShopContext.Foods
                            .Where(i => i.Url == foodName)
                            .FirstOrDefault();

        }

        public List<Food> GetFoodsByCategory(string name)
        {

            var foods = ShopContext.Foods.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                foods = foods.Include(i => i.FoodCategories)
                            .ThenInclude(i => i.Category)
                            .Where(i => i.FoodCategories.Any(a => a.Category.Name.ToLower() == name.ToLower()));
            }
            return foods.ToList();

        }

        public void Update(Food entity, int[] categroyIds)
        {

            var food = ShopContext.Foods
                                .Include(i => i.FoodCategories)
                                .FirstOrDefault(i => i.FoodId == entity.FoodId);
            if (food != null)
            {
                food.Name = entity.Name;
                food.Price = entity.Price;
                food.Description = entity.Description;
                food.ImageUrl = entity.ImageUrl;
                food.Url = entity.Url;
                food.FoodCategories = categroyIds.Select(catid => new FoodCategory()
                {
                    FoodId = entity.FoodId,
                    CategoryId = catid
                }).ToList();
            }

        }
    }
}