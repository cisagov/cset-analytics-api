using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.Interfaces;
using CsetAnalytics.ViewModels;
using CsetAnalytics.ViewModels.Login;
using CsetAnalytics.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;

namespace CsetAnalytics.Api.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/user")]
    public class UsersController : Controller
    {
        private readonly IConfigurationRoot _config;
        private readonly CsetContext _context;
        private readonly IUserBusiness _userBusiness;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(IConfigurationRoot config, CsetContext context, IUserBusiness userBusiness, RoleManager<IdentityRole> roleManager)
        {
            _config = config;
            _context = context;
            _userBusiness = userBusiness;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        [Route("getUsers")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userBusiness.GetAllUsers();

            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [Route("getUser/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {

            var user = await _userBusiness.GetUser(id);

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [Route("getRoles")]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _userBusiness.GetAllRoles();
            return Ok(roles);
        }

        [Authorize(Roles = "Admin")]
        [Route("getRole/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserRole(string id)
        {
            string userRole = await _userBusiness.GetUserRole(id);
            return Ok(userRole);
        }

        [Route("createUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] NewUser user)
        {
            try
            {
                var message = await _userBusiness.CreateUser(user);
                if (message.Errors == null)
                    return Ok(new List<string>{"User Created"});
                return BadRequest(message);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return BadRequest(new UserErrors() { Errors = new List<string>() { "User could not be created" }});
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("editUser")]
        [HttpPost]
        public async Task<IActionResult> EditUser([FromBody] EditUser user)
        {
            try
            {
                //string message = string.Empty;
                //if (!string.IsNullOrEmpty(user.Role))
                //{
                //    message += await _userBusiness.UpdateRole(user);
                //}
                //message += await _userBusiness.UpdateUser(user);
                //if (message == string.Empty)
                //{
                //    return Ok();
                //}
                return BadRequest("User could not be edited");
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return BadRequest("User could not be edited");
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("editChangePassword")]
        [HttpPost]
        public async Task<IActionResult> EditChangePassword([FromBody] EditUser user)
        {
            try
            {
                //string message = string.Empty;
                //if (!string.IsNullOrEmpty(user.NewPassword) && (user.NewPassword == user.ConfirmNewPassword))
                //{
                //    message = await _userBusiness.EditChangePassword(user);
                //}

                //if (message == string.Empty)
                //{
                //    return Ok();
                //}
                return BadRequest("User could not be edited");
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return BadRequest("User could not be edited");
            }
        }

        [Authorize]
        [Route("changePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                //    var user = await _userManager.FindByNameAsync(model.UserName);

                //    if (user != null)
                //    {
                //        if (await _userBusiness.PasswordCanBeUsed(user.Id, model.NewPassword))
                //        {
                //            if (model.NewPassword == model.ConfirmNewPassword)
                //            {
                //                var success = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                //                if (success.Succeeded)
                //                {
                //                    var applicationUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                //                    if (applicationUser != null)
                //                    {
                //                        var changePassword = await _userBusiness.SavePasswordChange(applicationUser, false);
                //                        await _userBusiness.SavePasswordHistory(applicationUser);
                //                        return Ok(changePassword);
                //                    }
                //                }
                //            }
                //        }
                //        else
                //        {
                //            return BadRequest("Password cannot be used again.");
                //        }
                //    }

                //    return BadRequest("Error occurred while changing password.");
                //}
                //else
                //{
                //    return BadRequest("Error occurred while changing password.");
                //}
                return Ok();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return BadRequest("Error occurred while changing password.");
            }
        }

        [Authorize]
        [Route("passwordExpired/{id}")]
        [HttpGet]
        public async Task<IActionResult> PasswordExpired(string id)
        {
            try
            {
                //var user = await _userManager.FindByNameAsync(id);

                //if (user != null)
                //{
                //    return Ok(await _userBusiness.PasswordExpired(user.Id));
                //}
                return Ok();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return BadRequest("Error occured while changing password.");
            }
        }

        [Authorize]
        [Route("checkPassword")]
        [HttpPost]
        public async Task<IActionResult> CheckPassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                //var user = await _userManager.FindByNameAsync(model.UserName);

                //if (user != null)
                //{
                //    return Ok(await _userBusiness.PasswordCanBeUsed(user.Id, model.NewPassword));
                //}
                return Ok(true);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return BadRequest("Error occured while changing password.");
            }
        }

        [Authorize]
        [Route("deleteUser")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromBody] EditUser editUser)
        {
            try
            {
                await _userBusiness.DeleteUser(editUser);
                return Ok();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return BadRequest("Error occured while deleting user");
            }
        }
    }
}
