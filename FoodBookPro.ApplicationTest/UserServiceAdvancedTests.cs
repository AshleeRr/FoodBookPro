using Moq;
using AutoMapper;
using FoodBookPro.Data.Application.Services;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Application.ViewModels.Users;
using FoodBookPro.Data.Domain.Entities;

public class UserServiceAdvancedTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UserService _userService;

    public UserServiceAdvancedTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _userService = new UserService(_userRepoMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task UpdateWithEncryption_Error_ReturnsFailure()
    {
        var saveVm = new SaveUserViewModel { UserName = "fail" };
        var entity = new User { UserName = "fail" };
        var repoResult = OperationResult<User>.Failure(message: "Update failed", errors: null, data: null);
        var mappedResult = OperationResult<UserViewModel>.Failure(message: "Update failed", errors: null, data: null);

        _mapperMock.Setup(m => m.Map<User>(saveVm)).Returns(entity);
        _userRepoMock.Setup(r => r.UpdateWithEncryptionAsync(entity, 2)).ReturnsAsync(repoResult);
        _mapperMock.Setup(m => m.Map<OperationResult<UserViewModel>>(repoResult)).Returns(mappedResult);

        var result = await _userService.UpdateWithEncryption(saveVm, 2);

        Assert.False(result.IsSuccess);
        Assert.Equal("Update failed", result.Message);
    }

    [Fact]
    public async Task Login_NullModel_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await _userService.Login(null);
        });
    }

    [Fact]
    public async Task UpdateWithEncryption_NullModel_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await _userService.UpdateWithEncryption(null, 1);
        });
    }

    [Fact]
    public async Task Login_RepositoryReturnsNull_ThrowsException()
    {
        var loginVm = new LoginViewModel { UserName = "nulluser", Password = "1234" };
        _userRepoMock.Setup(r => r.LoginAsync(loginVm)).ReturnsAsync((OperationResult<User>)null);

        await Assert.ThrowsAsync<NullReferenceException>(async () =>
        {
            await _userService.Login(loginVm);
        });
    }

    [Fact]
    public async Task GetByUsername_RepositoryReturnsNull_ThrowsException()
    {
        _userRepoMock.Setup(r => r.GetByUsernameAsync("nulluser")).ReturnsAsync((OperationResult<User>)null);

        await Assert.ThrowsAsync<NullReferenceException>(async () =>
        {
            await _userService.GetByUsername("nulluser");
        });
    }

    [Fact]
    public async Task UpdateWithEncryption_MapperReturnsNull_ThrowsException()
    {
        var saveVm = new SaveUserViewModel { UserName = "test" };
        _mapperMock.Setup(m => m.Map<User>(saveVm)).Returns((User)null);

        await Assert.ThrowsAsync<NullReferenceException>(async () =>
        {
            await _userService.UpdateWithEncryption(saveVm, 5);
        });
    }

    [Fact]
    public async Task GetByIdViewModel_InvalidId_ReturnsFailure()
    {
        var repoResult = OperationResult<User>.Failure(message: "Not found", errors: null, data: null);
        var mappedResult = OperationResult<UserViewModel>.Failure(message: "Not found", errors: null, data: null);

        _userRepoMock.Setup(r => r.GetByIdAsync(100)).ReturnsAsync(repoResult);
        _mapperMock.Setup(m => m.Map<OperationResult<UserViewModel>>(repoResult)).Returns(mappedResult);

        var result = await _userService.GetByIdViewModel(100);

        Assert.False(result.IsSuccess);
        Assert.Equal("Not found", result.Message);
    }

    [Fact]
    public async Task UpdateWithEncryption_CallsRepositoryMethodOnce()
    {
        var saveVm = new SaveUserViewModel { UserName = "callcheck" };
        var entity = new User { UserName = "callcheck" };
        var repoResult = OperationResult<User>.Success(entity);
        var mappedResult = OperationResult<UserViewModel>.Success(new UserViewModel { UserName = "callcheck" });

        _mapperMock.Setup(m => m.Map<User>(saveVm)).Returns(entity);
        _userRepoMock.Setup(r => r.UpdateWithEncryptionAsync(entity, 5)).ReturnsAsync(repoResult);
        _mapperMock.Setup(m => m.Map<OperationResult<UserViewModel>>(repoResult)).Returns(mappedResult);

        var result = await _userService.UpdateWithEncryption(saveVm, 5);

        _userRepoMock.Verify(r => r.UpdateWithEncryptionAsync(entity, 5), Times.Once);
        Assert.True(result.IsSuccess);
        Assert.Equal("callcheck", result.Data.UserName);
    }
}
