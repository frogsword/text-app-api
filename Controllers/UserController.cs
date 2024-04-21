using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TextApp.Data;
using TextApp.Dtos.MessageDtos;
using TextApp.Dtos.ProfileDtos;
using TextApp.Dtos.UserDtos;
using TextApp.Interfaces;
using TextApp.Mappers;
using TextApp.Models;
using TextApp.Repositories;

namespace TextApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IPasswordHasher<AppUser> passwordHasher;
        private readonly IPasswordValidator<AppUser> passwordValidator;
        private readonly IUserValidator<AppUser> userValidator;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IProfileInterface _profileRepo;

        public UserController
        (
            UserManager<AppUser> usrMgr,
            IPasswordHasher<AppUser> passwordHash,
            IPasswordValidator<AppUser> passwordVal,
            IUserValidator<AppUser> userValid,
            SignInManager<AppUser> signinMgr,
            IProfileInterface profileRepo
        )
        {
            passwordHasher = passwordHash;
            userManager = usrMgr;
            passwordValidator = passwordVal;
            userValidator = userValid;
            signInManager = signinMgr;
            _profileRepo = profileRepo;
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
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
                    //add profile for user
                    AppUser user = await userManager.FindByEmailAsync(appUser.Email);

                    CreateProfileDto profileModel = new()
                    {
                        UserId = user.Id,
                        Username = user.UserName,
                        Picture = null,
                        Contacts = null,
                        Blocks = null,
                        User = user,
                    };

                    var profile = profileModel.ToProfileFromCreateDto();

                    await _profileRepo.CreateAsync(profile);

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
