using AuthApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorizeController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthorizeController(RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(string userId, string roleName)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            IdentityRole role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);

            return StatusCode(201, roles);
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(string userId, string roleName)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            IdentityRole role = await _roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            return Ok(result);
        }
    }
}