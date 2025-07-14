using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;

namespace FoodBookPro.Data.Domain.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        //Task<OperationResult<List<Notification>>> GetNewNotifications();
        Task<OperationResult<bool>> MarkNotificationAsRead(int notificationId);
    }
}
