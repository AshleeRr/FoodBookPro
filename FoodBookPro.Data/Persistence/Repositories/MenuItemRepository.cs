using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodBookPro.Data.Persistence.Repositories
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationContext _context;
        public MenuItemRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OperationResult<bool>> ChangeMenuItemStatus(int menuItemId, bool state)
        {
            try
            {
                if (menuItemId <= 0)
                    return OperationResult<bool>.Failure("The id cannot be zero or minor than zero", null, false );


                var menuItem = await _context.Set<MenuItem>().FindAsync(menuItemId);
                
                if (menuItem == null)
                    return OperationResult<bool>.Success( false, "There is not a menu item with this id in the database");

                menuItem.Status = state;
                await _context.SaveChangesAsync();
                return OperationResult<bool>.Success(true, "Menu item status updated successfully");
            }
            catch (Exception e) 
            {
                return OperationResult<bool>.Failure($"Error changing the status of the menu item: {e.Message}", null, false);
            }
        }

        public async Task<OperationResult<List<MenuItem>>> GetMenuItemsByRestaurant(int restaurantId)
        {
            try
            {
                if (restaurantId <= 0)
                    return OperationResult<List<MenuItem>>.Failure("The id cannot be zero or minor than zero", null, new());

                var menuItems = await _context.Set<MenuItem>().Where(m => m.RestaurantId == restaurantId).ToListAsync();
                
                if (!menuItems.Any())
                    return OperationResult<List<MenuItem>>.Success(new List<MenuItem>(), "This restaurant does not contain any menu item");
                
                return OperationResult<List<MenuItem>>.Success(menuItems, "Menu items retrieved succesfully");
            }
            catch (Exception e)
            {
                return OperationResult<List<MenuItem>>.Failure($"Error retrieving the menu items of the restaurant: {e.Message}", null, new());
            }
        }
    }
}
