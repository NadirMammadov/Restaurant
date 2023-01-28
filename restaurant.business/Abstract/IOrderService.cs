using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.entity;

namespace restaurant.business.Abstract
{
    public interface IOrderService
    {
        void Create (Order entity);
        List<Order> GetAll();
        List<OrderItem> GetOrderDetails(int id);
    }
}