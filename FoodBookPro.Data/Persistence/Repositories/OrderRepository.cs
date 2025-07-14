using FoodBookPro.Data.Application.Enums;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodBookPro.Data.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationContext _context;
        public OrderRepository(ApplicationContext context) : base(context) { _context = context; }

        public async Task<OperationResult<int>> GetDailyOrderCount(int restaurantId, DateTime date)
        {
            try
            {
                if (restaurantId <= 0)
                    return OperationResult<int>.Failure("The id cannot be zero or minor than zero", null, 0);

                var count = await _context.Set<Order>().Where(o => o.RestaurantId == restaurantId && o.OrderDate == date).CountAsync();
                if (count == 0)
                {
                    return OperationResult<int>.Success(0, "This restaurant does not have any orders placed on the selected day");
                }

                return OperationResult<int>.Success(count, "Orders retrieved successfully");
            }
            catch (Exception e) 
            {
                return OperationResult<int>.Failure($"En error ocurred retrieving the daily orders:{e.Message}", null, 0);
            }
        }

        public async Task<OperationResult<List<(string ItemName, int TotalSold)>>> GetMostOrderedMenuItems(int restaurantId, int topN = 5)
        {
            try
            {
                if (restaurantId <= 0)
                    return OperationResult<List<(string ItemName, int TotalSold)>>.Failure("The id cannot be zero or minor than zero", null, new());

                var items = await _context.OrderDetails.Where(od => od.Order.RestaurantId == restaurantId && od.Order.Status == OrderStatus.Delivered)
                    .GroupBy(od => od.MenuItem.Name)
                    .Select(g => new
                    {
                        ItemName = g.Key,
                        TotalSold = g.Sum(x => x.Quantity)
                    })
                    .OrderByDescending(x => x.TotalSold)
                    .Take(topN)
                    .ToListAsync();

                if (!items.Any())
                    return OperationResult<List<(string ItemName, int TotalSold)>>.Success(new(), "This restaurant does not have any orders yet");
                
                var result = items.Select(x => (x.ItemName, x.TotalSold)).ToList();
                return OperationResult<List<(string ItemName, int TotalSold)>>.Success(result, "Ordered menu items retrieved successfully");

            }
            catch (Exception e) 
            {
                return OperationResult<List<(string ItemName, int TotalSold)>>.Failure($"An error ocurred retrieving the data: {e.Message}", null, new());
            }
        }
    }
}
