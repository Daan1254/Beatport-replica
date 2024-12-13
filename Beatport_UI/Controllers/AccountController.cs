using Microsoft.AspNetCore.Mvc;
using Beatport_BLL.Models.Dtos;
using Beatport_BLL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Beatport_UI.Models.Account;
using Beatport_BLL.Exceptions;

namespace Beatport_UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var result = await _userService.LoginUser(model);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }
            catch (UserServiceException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var result = await _userService.RegisterUser(model);
                if (result)
                {
                    return RedirectToAction("Login");
                }
                
                ModelState.AddModelError("", "Registration failed");
                return View(model);
            }
            catch (UserServiceException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            try
            {
                if (User.Identity?.IsAuthenticated == false)
                {
                    return RedirectToAction("Login");
                }

                string? email = User.Identity?.Name;
                if (string.IsNullOrEmpty(email))
                {
                    return RedirectToAction("Login");
                }

                UserProfileDto userProfile = await _userService.GetUserProfile(email);
                
                ProfileViewModel viewModel = new ProfileViewModel
                {
                    Email = userProfile.Email,
                    TotalSongs = userProfile.TotalSongs,
                    TotalPlaylists = userProfile.TotalPlaylists,
                    JoinDate = userProfile.JoinDate
                };
                
                return View(viewModel);
            }
            catch (NotFoundException)
            {
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
} 