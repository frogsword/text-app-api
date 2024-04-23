using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextApp.Interfaces;
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
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = Request.Cookies["user_id"];
                //set as secret
                var key = "v5fcvt72y03urf7g06ety8bfrdq75wtc";

                var decryptedString = AesService.DecryptString(key, userId);

                Profile profile = await _profileRepo.GetAsync(decryptedString);
                return Ok(profile);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("groups")]
        public async Task<IActionResult> RemoveGroup([FromBody] List<Guid> groupIds)
        {
            var userId = Request.Cookies["user_id"];
            //set as secret
            var key = "v5fcvt72y03urf7g06ety8bfrdq75wtc";

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
    }
}
