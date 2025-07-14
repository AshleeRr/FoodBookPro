using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodBookPro.Data.Persistence.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly ApplicationContext _context;
        public BookingRepository(ApplicationContext context) : base(context) 
        { 
            _context = context;
        }
        public async Task<OperationResult<List<Booking>>> GetAllBookingsOfAClient(int userId)
        {
            try
            {
                if (userId <= 0)
                    return OperationResult<List<Booking>>.Failure("The id can not have the value zero or minor", null, new());
                
                var bookings = await _context.Set<Booking>().Include(b => b.Restaurant).Include(b => b.Table)
                                      .Where(b => b.UserId == userId).ToListAsync();

                if(!bookings.Any())
                    return OperationResult<List<Booking>>.Success(new(), "This user does not have bookings yet");
                
                return OperationResult<List<Booking>>.Success(bookings,"Bookings retrieved successfully");

            }
            catch(Exception e){
                return OperationResult<List<Booking>>.Failure($"Error retieving the bookings from database: {e.Message}", null, new());
            }
        }
    }
}
