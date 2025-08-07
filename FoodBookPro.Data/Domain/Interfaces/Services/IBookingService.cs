using FoodBookPro.Data.Application.ViewModels.Bookings;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;

namespace FoodBookPro.Data.Domain.Interfaces.Services
{
    public interface IBookingService : IGenericService<SaveBookingViewModel, BookingViewModel, Booking>
    {
        Task<OperationResult<List<BookingViewModel>>> GetAllBookingsOfAClient(int userId);
    }
}
