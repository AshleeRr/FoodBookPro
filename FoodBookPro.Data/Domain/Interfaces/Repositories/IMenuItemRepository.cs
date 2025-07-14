using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Common;

namespace FoodBookPro.Data.Domain.Interfaces.Repositories
{
    public interface IMenuItemRepository : IGenericRepository<MenuItem>
    {
        Task<OperationResult<List<MenuItem>>> GetMenuItemsByRestaurant(int restaurantId);
        Task<OperationResult<bool>> ChangeMenuItemStatus(int menuItemId, bool state);
    }
}
