using AutoMapper;
using FoodBookPro.Data.Application.ViewModels.Payments;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Domain.Interfaces.Services;

namespace FoodBookPro.Data.Application.Services
{
    public class PaymentService : GenericService<SavePaymentViewModel, PaymentViewModel, Payment>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
            : base(paymentRepository, mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<PaymentViewModel>> SaveFrecuentPaymentMethod(int userId, string paymentMethod)
        {
            var result = await _paymentRepository.SaveFrecuentPaymentMethod(userId, paymentMethod);
            return _mapper.Map<OperationResult<PaymentViewModel>>(result);
        }

        public async Task<OperationResult<List<PaymentViewModel>>> GetFrecuentPaymentMethods(int userId)
        {
            var result = await _paymentRepository.GetFrecuentPaymentMethods(userId);
            return _mapper.Map<OperationResult<List<PaymentViewModel>>>(result);
        }
    }
}
