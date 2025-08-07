using AutoMapper;
using FoodBookPro.Data.Application.ViewModels.MenuItems;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Domain.Interfaces.Services;

namespace FoodBookPro.Data.Application.Services
{
    public class MenuItemService : GenericService<SaveMenuItemViewModel, MenuItemViewModel, MenuItem>, IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public MenuItemService(IMenuItemRepository menuItemRepository, IMapper mapper)
            : base(menuItemRepository, mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<MenuItemViewModel>>> GetMenuItemsByRestaurant(int restaurantId)
        {
            var result = await _menuItemRepository.GetMenuItemsByRestaurant(restaurantId);
            return _mapper.Map<OperationResult<List<MenuItemViewModel>>>(result);
        }

        public async Task<OperationResult<bool>> ChangeMenuItemStatus(int menuItemId, bool state)
        {
            return await _menuItemRepository.ChangeMenuItemStatus(menuItemId, state);
        }
    }
}

