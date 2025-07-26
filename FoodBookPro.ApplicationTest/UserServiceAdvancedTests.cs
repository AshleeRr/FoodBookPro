using Xunit;
using Moq;
using AutoMapper;
using FoodBookPro.Data.Application.Services;
using FoodBookPro.Data.Domain.Interfaces.Repositories;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Application.ViewModels.Users;
using FoodBookPro.Data.Domain.Entities;


//Basic Functionalities
public class UserServiceBasicTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UserService _userService;

    public UserServiceBasicTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _mapperMock = new Mock<IMapper>();
        _userService = new UserService(_userRepoMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsSuccess()
    {
        var loginVm = new LoginViewModel { UserName = "user", Password = "1234" };
        var repoResult = OperationResult<User>.Success(new User { Id = 1, UserName = "user" });
        var expectedResult = OperationResult<UserViewModel>.Success(new UserViewModel { Id = 1, UserName = "user" });

        _userRepoMock.Setup(r => r.LoginAsync(loginVm)).ReturnsAsync(repoResult);
        _mapperMock.Setup(m => m.Map<OperationResult<UserViewModel>>(repoResult)).Returns(expectedResult);

        var result = await _userService.Login(loginVm);

        Assert.True(result.IsSuccess);
        Assert.Equal("user", result.Data.UserName);
    }

    [Fact]
    public async Task Login_InvalidCredentials_ReturnsFailure()
    {
        var loginVm = new LoginViewModel { UserName = "wrong", Password = "wrong" };
        var repoResult = OperationResult<User>.Failure(message: "Invalid", errors: null, data: null);
        var expectedResult = OperationResult<UserViewModel>.Failure(message: "Invalid", errors: null, data: null);

        _userRepoMock.Setup(r => r.LoginAsync(loginVm)).ReturnsAsync(repoResult);
        _mapperMock.Setup(m => m.Map<OperationResult<UserViewModel>>(repoResult)).Returns(expectedResult);

        var result = await _userService.Login(loginVm);

        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid", result.Message);
    }

    [Fact]
    public async Task GetByUsername_ValidUsername_ReturnsUser()
    {
        var repoResult = OperationResult<User>.Success(new User { Id = 2, UserName = "test" });
        var mappedResult = OperationResult<UserViewModel>.Success(new UserViewModel { Id = 2, UserName = "test" });

        _userRepoMock.Setup(r => r.GetByUsernameAsync("test")).ReturnsAsync(repoResult);
        _mapperMock.Setup(m => m.Map<OperationResult<UserViewModel>>(repoResult)).Returns(mappedResult);

        var result = await _userService.GetByUsername("test");

        Assert.True(result.IsSuccess);
        Assert.Equal("test", result.Data.UserName);
    }

    [Fact]
    public async Task GetByUsername_UserNotFound_ReturnsFailure()
    {
        var repoResult = OperationResult<User>.Failure(message: "Not found", errors: null, data: null);
        var mappedResult = OperationResult<UserViewModel>.Failure(message: "Not found", errors: null, data: null);

        _userRepoMock.Setup(r => r.GetByUsernameAsync("ghost")).ReturnsAsync(repoResult);
        _mapperMock.Setup(m => m.Map<OperationResult<UserViewModel>>(repoResult)).Returns(mappedResult);

        var result = await _userService.GetByUsername("ghost");

        Assert.False(result.IsSuccess);
        Assert.Equal("Not found", result.Message);
    }

    [Fact]
    public async Task GetByIdViewModel_ValidId_ReturnsUser()
    {
        var repoResult = OperationResult<User>.Success(new User { Id = 3 });
        var mappedResult = OperationResult<UserViewModel>.Success(new UserViewModel { Id = 3 });

        _userRepoMock.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(repoResult);
        _mapperMock.Setup(m => m.Map<OperationResult<UserViewModel>>(repoResult)).Returns(mappedResult);

        var result = await _userService.GetByIdViewModel(3);

        Assert.True(result.IsSuccess);
        Assert.Equal(3, result.Data.Id);
    }

    [Fact]
    public async Task UpdateWithEncryption_ValidModel_ReturnsUpdatedUser()
    {
        var saveVm = new SaveUserViewModel { UserName = "updated" };
        var entity = new User { UserName = "updated" };
        var repoResult = OperationResult<User>.Success(entity);
        var mappedResult = OperationResult<UserViewModel>.Success(new UserViewModel { UserName = "updated" });

        _mapperMock.Setup(m => m.Map<User>(saveVm)).Returns(entity);
        _userRepoMock.Setup(r => r.UpdateWithEncryptionAsync(entity, 1)).ReturnsAsync(repoResult);
        _mapperMock.Setup(m => m.Map<OperationResult<UserViewModel>>(repoResult)).Returns(mappedResult);

        var result = await _userService.UpdateWithEncryption(saveVm, 1);

        Assert.True(result.IsSuccess);
        Assert.Equal("updated", result.Data.UserName);
    }

    [Fact]
    public async Task GetByIdViewModel_ValidId_ReturnsCorrectUserName()
    {
        var repoResult = OperationResult<User>.Success(new User { Id = 10, UserName = "john" });
        var mappedResult = OperationResult<UserViewModel>.Success(new UserViewModel { Id = 10, UserName = "john" });

        _userRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(repoResult);
        _mapperMock.Setup(m => m.Map<OperationResult<UserViewModel>>(repoResult)).Returns(mappedResult);

        var result = await _userService.GetByIdViewModel(10);

        Assert.True(result.IsSuccess);
        Assert.Equal("john", result.Data.UserName);

    }
}