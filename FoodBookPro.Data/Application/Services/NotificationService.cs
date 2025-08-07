using AutoMapper;
using FoodBookPro.Data.Application.ViewModels.Notifications;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Domain.Interfaces.Services;

namespace FoodBookPro.Data.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<bool>> MarkNotificationAsRead(int notificationId)
        {
            return await _notificationRepository.MarkNotificationAsRead(notificationId);
        }
    }
}

