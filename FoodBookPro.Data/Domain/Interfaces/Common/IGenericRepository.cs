using FoodBookPro.Data.Domain.Common;

namespace FoodBookPro.Data.Domain.Interfaces.Common
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<OperationResult<ICollection<TEntity>>> GetAllAsync();
        Task<OperationResult<ICollection<TEntity>>> GetAllIncludeAsync(ICollection<string> properties);
        Task<OperationResult<IQueryable<TEntity>>> GetAllQueryAsync();
        Task<OperationResult<IQueryable<TEntity>>> GetAllQueryIncludeAsync(ICollection<string> properties);
        Task<OperationResult<TEntity>> GetByIdAsync(int id);
        Task<OperationResult<TEntity>> AddAsync(TEntity entity);
        Task<OperationResult<ICollection<TEntity>>> AddRangeAsync(ICollection<TEntity> entity);
        Task<OperationResult<TEntity>> Update(TEntity entity);
        Task<OperationResult<TEntity>> RemoveAsync(int id);
    }
}
