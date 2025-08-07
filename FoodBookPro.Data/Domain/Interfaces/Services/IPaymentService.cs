uusing FoodBookPro.Data.Application.ViewModels.Payments;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;

namespace FoodBookPro.Data.Domain.Interfaces.Services
{
    public interface IPaymentService : IGenericService<SavePaymentViewModel, PaymentViewModel, Payment>
    {
        Task<OperationResult<PaymentViewModel>> SaveFrecuentPaymentMethod(int userId, string paymentMethod);
        Task<OperationResult<List<PaymentViewModel>>> GetFrecuentPaymentMethods(int userId);
    }
}
