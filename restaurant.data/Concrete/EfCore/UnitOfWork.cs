using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.data.Abstract;

namespace restaurant.data.Concrete.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _context;
        public UnitOfWork(ShopContext context)
        {
            _context =context;
        }
        private EfCoreCartRepository _cartRepository; 
        private EfCoreFoodRepository _foodRepository;
        private EfCoreCategoryRepository _categoryRepository;
        private EfCoreOrderRepository _orderRepository;
        public IFoodRepository Foods => _foodRepository = _foodRepository ?? new EfCoreFoodRepository(_context);

        public ICartRepository Carts =>_cartRepository = _cartRepository ?? new EfCoreCartRepository(_context);

        public ICategoryRepository Categories => _categoryRepository = _categoryRepository ?? new EfCoreCategoryRepository(_context);

        public IOrderRepository Orders => _orderRepository = _orderRepository ?? new EfCoreOrderRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}