using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels;
using CsetAnalytics.Interfaces;
using CsetAnalytics.ViewModels.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CsetAnalytics.Api.Controllers
{
    [Route("api/Login")]
    [ApiController]
    [EnableCors("CorsApi")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly CsetContext context;
        private readonly IUserBusiness userBusiness;

        public LoginController(IConfiguration config, CsetContext context, IUserBusiness userBusiness)
        {
            this.config = config;
            this.context = context;
            this.userBusiness = userBusiness;
        }

        /// <summary>
        /// Login and authorize.  Return JWT if authorized.
        /// </summary>
        /// <returns></returns>
        /// // GET: api/login/authenticate
        [Route("authenticate")]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] CredentialModel model)
        {

            try
            {

                //var user = await userManager.FindByNameAsync(model.UserName);

                //if (user != null)
                //{

                //    var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, true, true);
                //    Response.Cookies.Delete(".AspNetCore.Identity.Application");
                //    Response.Headers.Remove("Set-Cookie");
                //    if (result.Succeeded)
                //    {

                //        var roles = await userManager.GetRolesAsync(user);
                //        var claims = new[]
                //        {
                //            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //            new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                //        };

                //        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");


                //        //   claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, roles.FirstOrDefault()));
                //        var keyBytes = Encoding.ASCII.GetBytes(config["Tokens:Key"]);
                //        var key = new SymmetricSecurityKey(keyBytes);
                //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //        var changePassword = user.ChangePassword;
                //        var tokenDescriptor = new SecurityTokenDescriptor
                //        {
                //            Subject = claimsIdentity,
                //            Expires = DateTime.UtcNow.AddHours(int.Parse(config["Tokens:Timeout"])),
                //            Issuer = config["Tokens:Issuer"],
                //            Audience = config["Tokens:Audience"],
                //            SigningCredentials = creds
                //        };


                //        var tokenHandler = new JwtSecurityTokenHandler();
                //        var token = tokenHandler.CreateToken(tokenDescriptor);


                //        if (await userBusiness.PasswordExpired(user.Id))
                //        {
                //            changePassword = await userBusiness.SavePasswordChange(user, true);
                //        }

                //        return Ok(new
                //        {
                //            Token = tokenHandler.WriteToken(token),
                //            Expiration = token.ValidTo,
                //            user.UserName,
                //            ChangePassword = changePassword,
                //            Role = roles.FirstOrDefault()
                //        });

                //    }
                //    else if (result.IsLockedOut)
                //    {
                //        string minutesText = int.Parse(config["Login:DefaultLockoutTimeSpan"]) > 1
                //            ? "minutes"
                //            : "minute";
                //        return StatusCode(423,
                //            $"Account is locked.  Please try again in {config["Login:DefaultLockoutTimeSpan"]} {minutesText}.");
                //    }
                //    else if (result == Microsoft.AspNetCore.Identity.SignInResult.Failed)
                //    {

                //        return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden, "Failed to Login.");

                //    }
                //}
                //else
                //{
                //    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden, "Failed to Login.");
                //}
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured in Logging... ", ex);
                var error = ex.Message;
                return BadRequest($"An exception occurred in login. Error Message {error}");
            }
        }

        [Authorize]
        [Route("changePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var user = await userManager.FindByNameAsync(model.UserName);

                    //if (user != null)
                    //{
                    //    if (await userBusiness.PasswordCanBeUsed(user.Id, model.NewPassword))
                    //    {
                    //        if (model.NewPassword == model.ConfirmNewPassword)
                    //        {
                    //            var success = await userManager.ChangePasswordAsync(user, model.CurrentPassword,
                    //                model.NewPassword);
                    //            if (success.Succeeded)
                    //            {
                    //                var applicationUser = await context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                    //                if (applicationUser != null)
                    //                {
                    //                    var changePassword =
                    //                        await userBusiness.SavePasswordChange(applicationUser, false);
                    //                    await userBusiness.SavePasswordHistory(applicationUser);
                    //                    return Ok(changePassword);
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        return BadRequest("Password cannot be used again.");
                    //    }
                    //}

                    //return BadRequest("Error occurred while changing password.");
                    return Ok();
                }
                else
                {
                    return BadRequest("Error occurred while changing password.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured in Change Password... ", ex);
                var error = ex.Message;
                return BadRequest("Error occurred while changing password.");
            }


        }

        ////////////////AccessLog/////////////
        //UserLogin -- Authenticate()
        //UserChangePassword -- ChangePassword()
        //UserLogout
        [Route("logUserLogout")]
        [HttpGet]
        public ActionResult<bool> LogUserLogout()
        {
            try
            {
                var userName = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(true);
            }
            catch (InvalidDataException ex)
            {
                return StatusCode(422, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(422, ex.Message);
            }
        }
    }
}
