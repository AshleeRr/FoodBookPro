using FoodBookPro.Data.Application.ViewModels.Users;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Common;

namespace FoodBookPro.Data.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<OperationResult<User>> LoginAsync(LoginViewModel loginVm);

        Task<OperationResult<User>> GetByUsernameAsync(string username);

        Task<OperationResult<User>> UpdateWithEncryptionAsync(User entity, int id);
    }
}
