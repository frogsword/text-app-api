using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextApp.Interfaces;
using TextApp.Migrations;
using TextApp.Models;
using TextApp.Services;

namespace TextApp.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileInterface _profileRepo;
        private readonly IConfiguration _config;

        public ProfileController
        (
            IProfileInterface profileRepo,
            IConfiguration config
        )
        {
            _profileRepo = profileRepo;
            _config = config;
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = Request.Cookies["user_id"];
                //set as secret
                var key = _config["AesKey"];

                var decryptedString = AesService.DecryptString(key, userId);

                Profile profile = await _profileRepo.GetAsync(decryptedString);
                return Ok(profile);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("profile/username")]
        [Authorize]
        public async Task<IActionResult> UpdateUsername([FromBody] string username)
        {
            var userId = Request.Cookies["user_id"];
            //set as secret
            var key = _config["AesKey"];

            var decryptedString = AesService.DecryptString(key, userId);

            if (ModelState.IsValid && decryptedString != null)
            {
                await _profileRepo.UpdateUsernameAsync(decryptedString, username);

                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
