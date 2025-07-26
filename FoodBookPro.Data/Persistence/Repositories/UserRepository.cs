using FoodBookPro.Data.Application.Helpers;
using FoodBookPro.Data.Application.ViewModels.Users;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodBookPro.Data.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public virtual async Task<OperationResult<User>> GetByUsernameAsync(string username)
        {
            try
            {
                var entity = await _context.Set<User>().FindAsync(username);
                return entity != null
                    ? OperationResult<User>.Success(entity, "User found")
                    : OperationResult<User>.Failure("User not found", null, null);
            }
            catch (Exception e)
            {
                return OperationResult<User>.Failure($"Error getting User by UserName: {e.Message}", null, null);
            }
        }

        public virtual async Task<OperationResult<User>> LoginAsync(LoginViewModel loginVm)
        {
            try
            {
                string encryptedPassword = PasswordEncryptation.ComputeSha256Hash(loginVm.Password);
                var entity = await _context.Set<User>().FirstOrDefaultAsync(user => user.UserName == loginVm.UserName && user.Password == encryptedPassword);
                return entity != null
                    ? OperationResult<User>.Success(entity, "Login Successfull")
                    : OperationResult<User>.Failure("User name or password not found", null, null);
            }
            catch (Exception e)
            {
                return OperationResult<User>.Failure($"Error while validating the credentials: {e.Message}", null, null);
            }
        }

        public virtual async Task<OperationResult<User>> UpdateWithEncryptionAsync(User entity, int id)
        {
            entity.Password = PasswordEncryptation.ComputeSha256Hash(entity.Password);
            return await base.Update(entity);
        }
    }
}
