using FoodBookPro.Data.Domain.Common;

namespace FoodBookPro.Data.Domain.Interfaces.Services
{
    public interface INotificationService
    {
        Task<OperationResult<bool>> MarkNotificationAsRead(int notificationId);
    }
}
