using FoodBookPro.Data.Application.ViewModels.Orders;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;

namespace FoodBookPro.Data.Domain.Interfaces.Services
{
    public interface IOrderService : IGenericService<SaveOrderViewModel, OrderViewModel, Order>
    {
        Task<OperationResult<int>> GetDailyOrderCount(int restaurantId, DateTime date);
        Task<OperationResult<List<(string ItemName, int TotalSold)>>> GetMostOrderedMenuItems(int restaurantId, int topN = 5);
    }
}
