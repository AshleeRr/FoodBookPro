using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Persistence.Context;

namespace FoodBookPro.Data.Persistence.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        private readonly ApplicationContext _context;
        public RestaurantRepository(ApplicationContext context) : base(context) { _context = context; }
    }
}
