using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.entity;
using restaurant.data;
namespace restaurant.business.Abstract
{
    public interface IFoodService
    {
        Food GetById(int id);

        List<Food> GetAll();

        void Create(Food entity);

        void Update(Food entity);
        void Delete(Food entity);
        List<Food> GetFoodsByCategory (string name);
        Food GetByIdWithCategories(int foodId);
        void Update(Food entity, int[] categoryIds);
        Food GetByName(string foodName);
    }
}