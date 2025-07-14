using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Common;

namespace FoodBookPro.Data.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<OperationResult<int>> GetDailyOrderCount(int restaurantId, DateTime date);
        Task<OperationResult<List<(string ItemName, int TotalSold)>>> GetMostOrderedMenuItems(int restaurantId, int topN = 5);
        //Task<OperationResult<double>> GetMonthlyRevenue(int restaurantId, int month, int year);


    }
}
