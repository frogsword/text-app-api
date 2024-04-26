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

        public ProfileController
        (
            IProfileInterface profileRepo
        )
        {
            _profileRepo = profileRepo;
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = Request.Cookies["user_id"];
                //set as secret
                var key = Environment.GetEnvironmentVariable("AesKey");

                var decryptedString = AesService.DecryptString(key, userId);

                Profile profile = await _profileRepo.GetAsync(decryptedString);
                return Ok(profile);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("profile/groups")]
        [Authorize]
        public async Task<IActionResult> RemoveGroup([FromBody] List<Guid> groupIds)
        {
            var userId = Request.Cookies["user_id"];
            //set as secret
            var key = Environment.GetEnvironmentVariable("AesKey");

            var decryptedString = AesService.DecryptString(key, userId);

            if (ModelState.IsValid && decryptedString != null)
            {
                await _profileRepo.RemoveGroupFromProfileAsync(decryptedString, groupIds);

                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("profile/username")]
        [Authorize]
        public async Task<IActionResult> UpdateUsername([FromBody] string username)
        {
            var userId = Request.Cookies["user_id"];
            //set as secret
            var key = Environment.GetEnvironmentVariable("AesKey");

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
