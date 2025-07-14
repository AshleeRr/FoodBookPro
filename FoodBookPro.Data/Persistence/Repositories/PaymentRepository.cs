using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FoodBookPro.Data.Persistence.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public readonly ApplicationContext _context;
        public PaymentRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OperationResult<Payment>> SaveFrecuentPaymentMethod(int userId, string paymentMethod)
        {
            try
            {
                if(userId <= 0)
                {
                    return OperationResult<Payment>.Failure("The id cannot be zero or minor than zero", null, null);
                }
                if (string.IsNullOrEmpty(paymentMethod)) 
                {
                    return OperationResult<Payment>.Failure("The payment method must not be empty", null, null);
                }
                var payment = new Payment
                {
                    PaymentMethod = paymentMethod,
                    PaymentDate = DateTime.Now,
                    Amount = 0,
                    Status = false
                };
                await _context.SaveChangesAsync();
                return OperationResult<Payment>.Success(payment, "message");
            }catch(Exception e)
            {
                return OperationResult<Payment>.Failure($"Error trying to save the payment mehod: {e.Message}", null, null);
            }
        }

        public async Task<OperationResult<List<Payment>>> GetFrecuentPaymentMethods(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return OperationResult<List<Payment>>.Failure("The id cannot be zero or minor than zero", null, null);
                }
                var list = await _context.Payments
                    .Where(p => p.Order.UserId == userId)
                    .OrderByDescending(p => p.PaymentDate)
                    .ToListAsync();
                if (!list.Any())
                {
                    return OperationResult<List<Payment>>.Success(new(), "This user does not have any current payment methods saved");
                }
                return OperationResult<List<Payment>>.Success(list, "Frecuent payment methods retrieved successfully");
            }
            catch (Exception e)
            {
                return OperationResult<List<Payment>>.Failure($"Error retieving the payment method/s from database: {e.Message}", null, null);
            }
        }
    }
}
