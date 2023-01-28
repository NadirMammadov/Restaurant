using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.business.Abstract;
using restaurant.entity;
using restaurant.data.Abstract;
using restaurant.data.Concrete.EfCore;
namespace restaurant.business.Concrete
{
    public class FoodManager: IFoodService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        public FoodManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Create(Food entity)
        {
            _unitOfWork.Foods.Create(entity);
            _unitOfWork.Save();
        }

        public void Delete(Food entity)
        {
            _unitOfWork.Foods.Delete(entity);
            _unitOfWork.Save();
        }

        public List<Food> GetAll()
        {
            return _unitOfWork.Foods.GetAll();
        }

        public Food GetById(int id)
        {
            return _unitOfWork.Foods.GetById(id);
        }

        public Food GetByIdWithCategories(int foodId)
        {
            return _unitOfWork.Foods.GetByIdWithCategories(foodId);
        }

        public Food GetByName(string foodName)
        {
            return _unitOfWork.Foods.GetByName(foodName);
        }

        public List<Food> GetFoodsByCategory(string name)
        {
           return _unitOfWork.Foods.GetFoodsByCategory(name);
        }

        public void Update(Food entity)
        {
            _unitOfWork.Foods.Update(entity);
            _unitOfWork.Save();
        }

        public void Update(Food entity, int[] categoryIds)
        {
           _unitOfWork.Foods.Update(entity,categoryIds);
           _unitOfWork.Save();
        }
    }
}