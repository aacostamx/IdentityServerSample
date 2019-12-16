using AuthApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticateController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        //[HttpPost]
        //public async Task<ActionResult> GetTokenAsync()
        //{
        //    //var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.angular", "secret");
        //    //var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(model.UserName, model.Password, "api1 openid");


        //    return Ok();
        //}


        [HttpPost]
        public async Task<ActionResult> AuthenticateAsync([FromBody]LoginModel login)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync
                (login.UserName, login.Password, login.RememberMe, false);

            if (!result.Succeeded)
            {
                return StatusCode(500, "Invalid username or password");
            }

            return Ok(await _userManager.FindByNameAsync(login.UserName));
        }

        [HttpDelete]
        public async Task<ActionResult> SignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}