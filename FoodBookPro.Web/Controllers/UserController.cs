using FoodBookPro.Data.Application.ViewModels.Users;
using FoodBookPro.Data.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FoodBookPro.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            // Ejemplo: Obtener nombre de usuario de sesión
            var username = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            ViewBag.CurrentUser = username;

            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _userService.GetByIdViewModel(id);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(result.Data);
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _userService.Login(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Invalid login.");
                return View(vm);
            }

            // Guardar datos en sesión
            _httpContextAccessor.HttpContext.Session.SetString("UserName", result.Data.UserName);
            _httpContextAccessor.HttpContext.Session.SetInt32("UserId", result.Data.Id);
            _httpContextAccessor.HttpContext.Session.SetString("UserRole", result.Data.Role.ToString());

            TempData["SuccessMessage"] = "Login successful!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _userService.GetByIdViewModel(id);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            var vm = new SaveUserViewModel
            {
                FirstName = result.Data.FirstName,
                LastName = result.Data.LastName,
                UserName = result.Data.UserName,
                Email = result.Data.Email,
                Role = result.Data.Role,
                Status = result.Data.Status
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _userService.UpdateWithEncryption(vm, id);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Error updating user.");
                return View(vm);
            }

            TempData["SuccessMessage"] = "User updated successfully!";
            return RedirectToAction(nameof(Details), new { id });
        }

        public IActionResult Logout()
        {
            // Limpiar sesión
            _httpContextAccessor.HttpContext.Session.Clear();

            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction(nameof(Login));
        }
    }
}
