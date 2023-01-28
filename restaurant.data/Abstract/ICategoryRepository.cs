using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.entity;

namespace restaurant.data.Abstract
{
    public interface ICategoryRepository: IRepository<Category>
    {
        Category GetByIdWithFoods(int categoryId);
        void DeleteFromCategory(int foodId,int categoryId);
    }
}