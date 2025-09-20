using GerenciadorCursos.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCursos.Application.Services
{
    public class AuthService : IAuth
    {
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly UserManager<IdentityUser> _userManager;    
        public AuthService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> Auth(string email, string password)
        {
            var result = await _signManager.PasswordSignInAsync(email, password, false, lockoutOnFailure
                : false);
            return result.Succeeded;
        }

        // Registra user e loga
        public async Task<bool> Register(string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded) 
            { 
                await _signManager.SignInAsync(user, isPersistent: false);
            }

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signManager.SignOutAsync();
        }

        
    }
}
