using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_roleManager.Roles.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string name)
        {
            var role = new IdentityRole(name);
            IdentityResult result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            return StatusCode(201, role);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]IdentityRole role)
        {
            IdentityRole roleEntity = await _roleManager.FindByIdAsync(role.Id);

            if (roleEntity == null)
            {
                return NotFound();
            }

            roleEntity.Name = role.Name ?? roleEntity.Name;

            IdentityResult result = await _roleManager.UpdateAsync(roleEntity);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            return Ok(roleEntity);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAsyn(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            IdentityResult result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            return Ok(result);
        }
    }
}