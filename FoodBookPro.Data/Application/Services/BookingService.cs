using AutoMapper;
using FoodBookPro.Data.Application.ViewModels.Bookings;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Domain.Interfaces.Services;

namespace FoodBookPro.Data.Application.Services
{
    public class BookingService : GenericService<SaveBookingViewModel, BookingViewModel, Booking>, IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
            : base(bookingRepository, mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<BookingViewModel>>> GetAllBookingsOfAClient(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("El ID no puede ser menor o igual a cero", nameof(userId));

            var result = await _bookingRepository.GetAllBookingsOfAClient(userId);

            return _mapper.Map<OperationResult<List<BookingViewModel>>>(result);
        }
    }
}
