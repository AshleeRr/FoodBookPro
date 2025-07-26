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

        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<UserViewModel>>? Login(LoginViewModel vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm), "Login model cannot be null.");

            var result = await _userRepository.LoginAsync(vm);
            if (result == null)
                throw new NullReferenceException("Repository returned null for LoginAsync.");

            return _mapper.Map<OperationResult<UserViewModel>>(result);
        }

        public async Task<OperationResult<UserViewModel>> GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username), "Username cannot be null or empty.");

            var result = await _userRepository.GetByUsernameAsync(username);
            if (result == null)
                throw new NullReferenceException("Repository returned null for GetByUsernameAsync.");

            return _mapper.Map<OperationResult<UserViewModel>>(result);
        }

        public async Task<OperationResult<UserViewModel>> GetByIdViewModel(int id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            if (result == null)
                throw new NullReferenceException("Repository returned null for GetByIdAsync.");

            return _mapper.Map<OperationResult<UserViewModel>>(result);
        }

        public async Task<OperationResult<UserViewModel>> UpdateWithEncryption(SaveUserViewModel vm, int id)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm), "User model cannot be null.");

            var entity = _mapper.Map<User>(vm);
            if (entity == null)
                throw new NullReferenceException("Mapper returned null when mapping SaveUserViewModel to User.");

            var result = await _userRepository.UpdateWithEncryptionAsync(entity, id);
            if (result == null)
                throw new NullReferenceException("Repository returned null for UpdateWithEncryptionAsync.");

            return _mapper.Map<OperationResult<UserViewModel>>(result);
        }
    }
}
