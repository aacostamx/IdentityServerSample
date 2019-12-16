using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class ClientController : Controller
    {
        [Authorize]
        public async Task<ActionResult> AccessToken()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");

            return Json(accessToken);
        }

        [Authorize]
        public async Task<ActionResult> GetUserAsync()
        {

            using var client = new HttpClient();

            TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5000/connect/token",
                ClientId = "client",
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",

                Scope = "auth_api"
            });

            using var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            HttpResponseMessage response = await apiClient.GetAsync("https://localhost:5001/api/user");
            string content = await response.Content.ReadAsStringAsync();
            return Json(content);
        }
    }
}