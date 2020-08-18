using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using CsetAnalytics.DomainModels;
using CsetAnalytics.DomainModels.Models;
using CsetAnalytics.Enums;
using CsetAnalytics.Interfaces;
using CsetAnalytics.ViewModels;
using CsetAnalytics.ViewModels.Users;

namespace CsetAnalytics.Business
{
    public class UsersBusiness : IUserBusiness
    {
        //private readonly CsetContext _context;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        //private readonly RoleManager<IdentityRole> _roleManager;

        public UsersBusiness()
        {
            //_context = context;
            //_userManager = userManager;
            //_passwordHasher = passwordHasher;
            //_roleManager = roleManager;
        }

        /// <summary>
        /// Get user by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EditUser> GetUser(string id)
        {
            try
            {

                //    var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
                //    var role = await _userManager.GetRolesAsync(user);
                //    var editUser = new EditUser
                //    {

                //        Id = user.Id,
                //        UserName = user.UserName,
                //        Email = user.Email,
                //        Role = role.FirstOrDefault(),

                //    };
                //    return editUser;
                return new EditUser();
            }

            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                //var users = from user in _context.Users
                //            join userRole in _context.UserRoles on user.Id equals userRole.UserId
                //            join role in _context.Roles on userRole.RoleId equals role.Id
                //            select new User
                //            {
                //                Id = user.Id,
                //                UserName = user.UserName,
                //                Email = user.Email,
                //                ChangePassword = user.ChangePassword,
                //                Role = role.Name
                //            };
                //return await users.ToListAsync();
                return new List<User>();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }

        }

        /// <summary>
        /// Get user role by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetUserRole(string id)
        {
            //var userRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == id);
            //if (userRole != null)
            //{
            //    var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
            //    return role.Name;
            //}
            return null;
        }

        /// <summary>
        /// Get all available roles
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetAllRoles()
        {
            try
            {
                //var roles = _context.Roles.Select(x => x.Name);
                //return await roles.ToListAsync();
                return new List<string>();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return null;
            }

        }

        /// <summary>
        /// Create a new user in the system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserErrors> CreateUser(NewUser user)
        {
            try
            {
                //if (_userManager.FindByNameAsync
                //    (user.UserName).Result == null)
                //{
                //    ApplicationUser appUser = new ApplicationUser();
                //    appUser.UserName = user.UserName;
                //    appUser.Email = user.Email.ToLower();
                //    appUser.ChangePassword = true;
                //    IdentityResult result = await _userManager.CreateAsync(appUser, user.NewPassword);
                //    if (result.Succeeded)
                //    {
                //        user.Role = RolesEnum.User.ToString();
                //        var roleSuccess = await _userManager.AddToRoleAsync(appUser, user.Role);
                //    }

                //    var message = new UserErrors
                //        {Errors = result.Errors.Any() ? result.Errors.Select(x => x.Description).ToList() : null};

                //    return message;
                //}
                //return (new UserErrors { Errors = new List<string>{"User already exists."}});
                return new UserErrors();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return (new UserErrors { Errors = new List<string> { "Username could not be created" } });
            }

        }

        /// <summary>
        /// Update a user record
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> UpdateUser(EditUser user)
        {
            try
            {
                //string message = string.Empty;
                //var appUser = await _userManager.FindByNameAsync(user.UserName);
                //if (appUser != null)
                //{
                //    appUser.UserName = user.UserName;

                //    IdentityResult result = _userManager.UpdateAsync(appUser).Result;
                //    return "";
                //}
                //return ("User could not be edited");
                return string.Empty;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return ("Username could not be edited");
            }
        }

        /// <summary>
        /// Update role of user
        /// </summary>
        /// <param name="editUser"></param>
        /// <returns></returns>
        public async Task<string> UpdateRole(EditUser editUser)
        {
            try
            {
                //string message = string.Empty;
                //var user = await _userManager.FindByNameAsync(editUser.UserName);

                //if (user != null)
                //{
                //    var role = await _userManager.GetRolesAsync(user);
                //    await _userManager.RemoveFromRoleAsync(user, role.FirstOrDefault());
                //    var success = await _userManager.AddToRoleAsync(user, editUser.Role);
                //    if (success == IdentityResult.Success)
                //    {
                //        return string.Empty;
                //    }
                //}
                //return "Could not update the role";
                return string.Empty;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return ("Could not update the role");
            }
        }

        /// <summary>
        /// Check if password has expired
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> PasswordExpired(string userId)
        {
            //var maxPasswordAge = await _context.Configurations.FirstOrDefaultAsync(x => x.ConfigurationKey == "MaxPasswordAge");
            //var lastPasswordDate = await _context.PasswordHistories.Where(x => x.AspNetUserId == userId).OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
            //int maxPassAgeConverted = default(int);
            //if (maxPasswordAge != null && lastPasswordDate != null && Int32.TryParse(maxPasswordAge.ConfigurationValue, out maxPassAgeConverted))
            //{
            //    var startDate = lastPasswordDate.CreatedDate;
            //    var expiryDate = startDate.AddDays(maxPassAgeConverted);
            //    if (DateTime.Now > expiryDate)
            //    {
            //        return true;
            //    }
            //}

            return false;
        }

        /// <summary>
        /// Check if password can be used based on history
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<bool> PasswordCanBeUsed(string userId, string newPassword)
        {

            //var user = await _userManager.FindByIdAsync(userId);
            //if (user != null)
            //{
            //    var historyCountConfig = await _context.Configurations.FirstOrDefaultAsync(x => x.ConfigurationKey == "PasswordHistoryRemembered");
            //    int historyCount = 25;
            //    if (historyCountConfig != null)
            //    {
            //        historyCount = Int32.Parse(historyCountConfig.ConfigurationValue);
            //    }
            //    var passwords = _context.PasswordHistories.Where(x => x.AspNetUserId == user.Id).Take(historyCount);
            //    foreach (var p in passwords)
            //    {
            //        if (_passwordHasher.VerifyHashedPassword(user, p.PasswordHash, newPassword) != PasswordVerificationResult.Failed)
            //        {
            //            return false;
            //        }
            //    }
            //}

            return true;
        }

        /// <summary>
        /// Save password to history
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        public async Task SavePasswordHistory()
        {
            //PasswordHistory passwordHistory = new PasswordHistory
            //{
            //    PasswordHash = applicationUser.PasswordHash,
            //    AspNetUserId = applicationUser.Id,
            //    CreatedDate = DateTime.Now,
            //    CreatedUserId = applicationUser.Id
            //};
            //_context.PasswordHistories.Add(passwordHistory);
            //await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Save user password to user 
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        public async Task<bool> SavePasswordChange(bool changePassword)
        {

            //applicationUser.ChangePassword = changePassword;
            //await _context.SaveChangesAsync();
            //return applicationUser.ChangePassword;
            return true;

        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="editUser"></param>
        /// <returns></returns>
        public async Task<string> EditChangePassword(EditUser editUser)
        {
            try
            {
                //string message = string.Empty;
                //var user = await _userManager.FindByNameAsync(editUser.UserName);

                //if (user != null)
                //{

                //    if (editUser.NewPassword == editUser.ConfirmNewPassword)
                //    {
                //        var cts = new CancellationTokenSource();
                //        var token = cts.Token;
                //        var newPasswordHash = _passwordHasher.HashPassword(user, editUser.NewPassword);
                //        user.PasswordHash = newPasswordHash;
                //        user.ChangePassword = true;
                //        var result = await _userManager.UpdateAsync(user);
                //        if (result.Succeeded)
                //        {
                //            var applicationUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                //            if (applicationUser != null)
                //            {
                //                var changePassword = await SavePasswordChange(applicationUser, false);
                //                //await SavePasswordHistory(applicationUser);
                //                return message;
                //            }
                //        }
                //    }


                //}

                //return "User could not be edited";
                return string.Empty;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return "User could not be edited";
            }
        }

        /// <summary>
        /// Delete user and associated records
        /// </summary>
        /// <param name="editUser"></param>
        /// <returns></returns>
        public async Task DeleteUser(EditUser editUser)
        {
            //var user = await _userManager.FindByNameAsync(editUser.UserName);
            //var rolesForUser = await _userManager.GetRolesAsync(user);

            //using (var transaction = _context.Database.BeginTransaction())
            //{
            //    var history = _context.PasswordHistories.Where(x => x.AspNetUserId == user.Id);
                

            //    foreach (var h in history)
            //    {
            //        _context.PasswordHistories.Remove(h);
            //    }
                
            //    _context.SaveChanges();

            //    if (rolesForUser.Count() > 0)
            //    {
            //        foreach (var item in rolesForUser.ToList())
            //        {
            //            // item should be the name of the role
            //            var result = await _userManager.RemoveFromRoleAsync(user, item);
            //        }
            //    }

            //    await _userManager.DeleteAsync(user);
            //    transaction.Commit();
            //}
        }


    }
}
