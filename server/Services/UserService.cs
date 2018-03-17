using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kefcon.Data;
using Kefcon.Entities;
using Kefcon.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kefcon.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> Authenticate(string username, string password);
        void Logout();
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser GetById(Guid id);
        IEnumerable<string> Create(ApplicationUser user, string password);
        void Update(ApplicationUser user);
        void Delete(Guid id);
        Task ConfirmEmailAsync(ApplicationUser user, string code);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<ApplicationUser> Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var result = await _signInManager.PasswordSignInAsync(email, password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = _userManager.Users.SingleOrDefault(a => a.Email == email);
                return user;
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _userManager.Users;
        }

        public ApplicationUser GetById(Guid id)
        {
            return _userManager.Users.SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<string> Create(ApplicationUser user, string password)
        {
            var result = _userManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                _signInManager.SignInAsync(user, false);
                return null;
            }
            else {
                return result.Errors.Select(e => e.Description);
            }
        }

        public void Update(ApplicationUser user)
        {
            // for later, when user management is implemented
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            // Probably will do a disable instead
            throw new NotImplementedException();
        }

        public Task ConfirmEmailAsync(ApplicationUser user, string code)
        {
            return _userManager.ConfirmEmailAsync(user, code);
        }
    }
}