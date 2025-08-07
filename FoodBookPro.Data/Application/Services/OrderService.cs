using AutoMapper;
using FoodBookPro.Data.Application.ViewModels.Orders;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Domain.Interfaces.Services;

namespace FoodBookPro.Data.Application.Services
{
    public class OrderService : GenericService<SaveOrderViewModel, OrderViewModel, Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
            : base(orderRepository, mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<int>> GetDailyOrderCount(int restaurantId, DateTime date)
        {
            return await _orderRepository.GetDailyOrderCount(restaurantId, date);
        }

        public async Task<OperationResult<List<(string ItemName, int TotalSold)>>> GetMostOrderedMenuItems(int restaurantId, int topN = 5)
        {
            return await _orderRepository.GetMostOrderedMenuItems(restaurantId, topN);
        }
    }
}
