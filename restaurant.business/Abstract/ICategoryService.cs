using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.entity;

namespace restaurant.business.Abstract
{
    public interface ICategoryService
    {
        Category GetById(int id);

        List<Category> GetAll();

        void Create(Category entity);

        void Update(Category entity);
        void Delete(Category entity);
        Category GetByIdWithFoods(int categoryId);
        void DeleteFromCategory(int foodId,int categoryId);
    }
}