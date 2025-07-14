using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Persistence.Context;

namespace FoodBookPro.Data.Persistence.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        private readonly ApplicationContext _context;
        public NotificationRepository(ApplicationContext context) : base(context) { _context = context; }

        public async Task<OperationResult<bool>> MarkNotificationAsRead(int notificationId)
        {
            try
            {
                if (notificationId <= 0) 
                    return OperationResult<bool>.Failure("The id cannot be zero or minor than zero", null, false); 

                var notification = await _context.Set<Notification>().FindAsync(notificationId);
                
                if (notification == null)
                    return OperationResult<bool>.Success(false, "It does not exists a notification with this id");

                if (notification.IsRead)
                    return OperationResult<bool>.Success(false, "This notification is already marked as read");

                notification.IsRead = true;
                await _context.SaveChangesAsync();

                return OperationResult<bool>.Success(true, "Notification marked as read successfully");
            }
            catch (Exception e)
            {
                return OperationResult<bool>.Failure($"An error ocurred trying to mark the notification as read: {e.Message}", null, false);
            }

        }
    }
}
