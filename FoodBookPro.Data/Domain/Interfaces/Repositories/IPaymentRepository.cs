using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Common;

namespace FoodBookPro.Data.Domain.Interfaces.Repositories
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<OperationResult<Payment>> SaveFrecuentPaymentMethod(int userId, string paymentMethod);
        Task<OperationResult<List<Payment>>> GetFrecuentPaymentMethods(int userId);
    }
}
