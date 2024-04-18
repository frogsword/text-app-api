using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TextApp.Dtos.UserDtos;
using TextApp.Models;

namespace TextApp.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;
        private IPasswordValidator<AppUser> passwordValidator;
        private IUserValidator<AppUser> userValidator;
        private SignInManager<AppUser> signInManager;

        public UserController
        (
            UserManager<AppUser> usrMgr,
            IPasswordHasher<AppUser> passwordHash,
            IPasswordValidator<AppUser> passwordVal,
            IUserValidator<AppUser> userValid,
            SignInManager<AppUser> signinMgr
        )
        {
            passwordHasher = passwordHash;
            userManager = usrMgr;
            passwordValidator = passwordVal;
            userValidator = userValid;
            signInManager = signinMgr;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByEmailAsync(loginDto.Email);
                if (appUser != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, loginDto.Password, false, false);
                    if (result.Succeeded)
                        return Ok(appUser);
                }
                ModelState.AddModelError(nameof(loginDto.Email), "Login Failed: Invalid Email or password");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                if (registerDto.Password != registerDto.ConfirmPassword)
                {
                    return BadRequest("Passwords must match.");
                }

                AppUser appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                IdentityResult result = await userManager.CreateAsync(appUser, registerDto.Password);

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("This email is already registered with an account. Please login.");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
