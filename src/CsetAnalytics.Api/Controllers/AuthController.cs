using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Auth.AccessControlPolicy.ActionIdentifiers;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Mvc;
using CsetAnalytics.DomainModels.Models;
using Microsoft.Extensions.Options;

namespace CsetAnalytics.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IOptions<CognitoSettings> cognitoSettings;

        public AuthController(IOptions<CognitoSettings> cognitoSettings)
        {
            this.cognitoSettings = cognitoSettings;
        }

        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<string>> Register(User user)
        {
            RegionEndpoint region = RegionEndpoint.GetBySystemName(this.cognitoSettings.Value.Region);
            var cognito = new AmazonCognitoIdentityProviderClient(region);

            var request = new SignUpRequest
            {
                ClientId = this.cognitoSettings.Value.AppClientId,
                Password = user.Password,
                Username = user.Username
            };

            var emailAttribute = new AttributeType
            {
                Name = "email",
                Value = user.Email
            };
            request.UserAttributes.Add(emailAttribute);

            var response = await cognito.SignUpAsync(request);

            return Ok();
        }

        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult<string>> SignIn(User user)
        {
            RegionEndpoint region = RegionEndpoint.GetBySystemName(this.cognitoSettings.Value.Region);
            var cognito = new AmazonCognitoIdentityProviderClient(region);

            var request = new AdminInitiateAuthRequest
            {
                UserPoolId = this.cognitoSettings.Value.PoolId,
                ClientId = this.cognitoSettings.Value.AppClientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
            };

            request.AuthParameters.Add("USERNAME", user.Username);
            request.AuthParameters.Add("PASSWORD", user.Password);

            var response = await cognito.AdminInitiateAuthAsync(request);
            var expireDate = DateTime.Now.AddSeconds(response.AuthenticationResult.ExpiresIn);
            return Ok(new
            {
                id_token = response.AuthenticationResult.IdToken,
                expires_at = expireDate,
                username = user.Username
            });
        }
    }
}
