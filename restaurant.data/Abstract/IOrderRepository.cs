using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.entity;

namespace restaurant.data.Abstract
{
    public interface IOrderRepository: IRepository<Order>
    {
        List<OrderItem> GetOrderDetails(int id);
    }
}