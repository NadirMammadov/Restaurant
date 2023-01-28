using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using restaurant.data.Abstract;
using restaurant.entity;

namespace restaurant.data.Concrete.EfCore
{
    public class EfCoreOrderRepository : EfCoreGenericRepository<Order>, IOrderRepository
    {
        public EfCoreOrderRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }
        public List<OrderItem> GetOrderDetails(int id)
        {
           
                return ShopContext.OrderItems.Where(o => o.OrderId == id)
                    .Include(f=>f.Food)
                    .ToList();
          
        }
    }
}