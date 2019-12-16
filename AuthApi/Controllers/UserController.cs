using AuthApi.Models;
using AuthApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult<ApiResponse> GetAll()
        {
            var reponse = new ApiResponse
            {
                Data = _userManager.Users.ToList()
            };

            return Ok(reponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetByIdAsync(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<IdentityResult>> CreateAsync(string userName, string password, string email)
        {
            var user = new ApplicationUser { UserName = userName, Email = email };
            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }
            return StatusCode(201, user);
        }

        [HttpPut]
        public async Task<ActionResult<ApplicationUser>> UpdateAsync([FromBody]ApplicationUser user)
        {
            ApplicationUser app = await _userManager.FindByIdAsync(user.Id);

            if (app == null)
            {
                return NotFound();
            }

            app.UserName = user.UserName ?? app.UserName;
            app.Email = user.Email ?? app.Email;
            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                app.PasswordHash = _userManager.PasswordHasher.HashPassword(app, user.PasswordHash);
            }

            IdentityResult result = await _userManager.UpdateAsync(app);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            return Ok(app);
        }

        [HttpDelete]
        public async Task<ActionResult<ApplicationUser>> DeleteAsync(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            return Ok(result);
        }
    }
}