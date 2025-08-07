using FoodBookPro.Data.Application.ViewModels.MenuItems;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;

namespace FoodBookPro.Data.Domain.Interfaces.Services
{
    public interface IMenuItemService : IGenericService<SaveMenuItemViewModel, MenuItemViewModel, MenuItem>
    {
        Task<OperationResult<List<MenuItemViewModel>>> GetMenuItemsByRestaurant(int restaurantId);
        Task<OperationResult<bool>> ChangeMenuItemStatus(int menuItemId, bool state);
    }
}
