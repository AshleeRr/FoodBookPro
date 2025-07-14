using FoodBookPro.Data.Domain.Common;
using System.Security.Cryptography;

namespace FoodBookPro.Data.Domain.Interfaces.Common
{
    public interface IGenericService<SaveVm, Vm, TEntity> 
        where SaveVm : class
        where Vm : class
        where TEntity : class
    {
        Task<OperationResult<ICollection<Vm>>> GetAllAsync();
        Task<OperationResult<ICollection<Vm>>> GetAllIncludeAsync(ICollection<string> properties);
        Task<OperationResult<IQueryable<Vm>>> GetAllQueryAsync();
        Task<OperationResult<IQueryable<Vm>>> GetAllQueryIncludeAsync(ICollection<string> properties);
        Task<OperationResult<Vm>> GetByIdAsync(int id);
        Task<OperationResult<Vm>> AddAsync(SaveVm vm);
        Task<OperationResult<ICollection<Vm>>> AddRangeAsync(ICollection<SaveVm> vm);
        Task<OperationResult<Vm>> Update(SaveVm vm);
        Task<OperationResult<Vm>> RemoveAsync(int id);
    }
}
