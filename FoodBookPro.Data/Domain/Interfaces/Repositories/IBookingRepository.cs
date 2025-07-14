using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Common;

namespace FoodBookPro.Data.Domain.Interfaces.Repositories
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<OperationResult<List<Booking>>> GetAllBookingsOfAClient(int userId);
    }
}
