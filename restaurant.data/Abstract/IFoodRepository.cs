using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.entity;

namespace restaurant.data.Abstract
{
    public interface IFoodRepository: IRepository<Food>
    {
        List<Food> GetFoodsByCategory (string name);
        Food GetByIdWithCategories(int foodId);
        void Update(Food entity,int[] categroyIds);
        Food GetByName(string foodName);
    }
}