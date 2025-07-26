using Azure;
using FoodBookPro.Data.Application.ViewModels.Users;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Common;

namespace FoodBookPro.Data.Domain.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel, User>
    {
        Task<OperationResult<UserViewModel>>? Login(LoginViewModel vm);

        Task<OperationResult<UserViewModel>> GetByUsername(string username);

        Task<OperationResult<UserViewModel>> GetByIdViewModel(int id);
        Task<OperationResult<UserViewModel>> UpdateWithEncryption(SaveUserViewModel vm, int id);
    }
}
