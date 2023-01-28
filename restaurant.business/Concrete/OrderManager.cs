using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.business.Abstract;
using restaurant.data.Abstract;
using restaurant.entity;

namespace restaurant.business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IUnitOfWork _unitOfWork;
        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Order entity)
        {
            _unitOfWork.Orders.Create(entity);
            _unitOfWork.Save();
        }

        public List<Order> GetAll()
        {
           return _unitOfWork.Orders.GetAll();
        }

        public List<OrderItem> GetOrderDetails(int id)
        {
           return _unitOfWork.Orders.GetOrderDetails(id);
        }
    }
}