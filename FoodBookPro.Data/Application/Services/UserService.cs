using AutoMapper;
using FoodBookPro.Data.Application.ViewModels.Users;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Domain.Interfaces.Services;

namespace FoodBookPro.Data.Application.Services
{
    public class UserService : GenericService<SaveUserViewModel, UserViewModel, User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
            : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<UserViewModel>> Login(LoginViewModel vm)
        {
            if (vm == null)
                return OperationResult<UserViewModel>.Failure("Login model cannot be null.", null, default);

            var result = await _userRepository.LoginAsync(vm);

            if (result == null || !result.IsSuccess)
                return OperationResult<UserViewModel>.Failure(result?.Message ?? "Invalid credentials.", result?.Errors, default);

            return MapOperationResult(result);
        }

        public async Task<OperationResult<UserViewModel>> GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return OperationResult<UserViewModel>.Failure("Username cannot be null or empty.", null, default);

            var result = await _userRepository.GetByUsernameAsync(username);

            if (result == null || !result.IsSuccess)
                return OperationResult<UserViewModel>.Failure(result?.Message ?? "User not found.", result?.Errors, default);

            return MapOperationResult(result);
        }

        public async Task<OperationResult<UserViewModel>> GetByIdViewModel(int id)
        {
            var result = await _userRepository.GetByIdAsync(id);

            if (result == null || !result.IsSuccess)
                return OperationResult<UserViewModel>.Failure(result?.Message ?? $"User with ID {id} not found.", result?.Errors, default);

            return MapOperationResult(result);
        }

        public async Task<OperationResult<UserViewModel>> UpdateWithEncryption(SaveUserViewModel vm, int id)
        {
            if (vm == null)
                return OperationResult<UserViewModel>.Failure("User model cannot be null.", null, default);

            var entity = _mapper.Map<User>(vm);
            if (entity == null)
                return OperationResult<UserViewModel>.Failure("Error mapping SaveUserViewModel to User entity.", null, default);

            var result = await _userRepository.UpdateWithEncryptionAsync(entity, id);

            if (result == null || !result.IsSuccess)
                return OperationResult<UserViewModel>.Failure(result?.Message ?? $"Failed to update user with ID {id}.", result?.Errors, default);

            return MapOperationResult(result);
        }

        /// <summary>
        /// Mapea un OperationResult<User> a OperationResult<UserViewModel>.
        /// </summary>
        private OperationResult<UserViewModel> MapOperationResult(OperationResult<User> result)
        {
            return OperationResult<UserViewModel>.Success(
                _mapper.Map<UserViewModel>(result.Data),
                result.Message
            );
        }
    }
}
