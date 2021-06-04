using BL.Bases;
using BL.Helper;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace BL.Repositories
{
    public class AccountRepositroy
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly IConfiguration Configuration;


        public AccountRepositroy(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Configuration = configuration;
        }

        public async Task<Response> Register(RegisterModel userModel)
        {
            var CheckUser = await UserManager.FindByNameAsync(userModel.Name);

            if (CheckUser != null )
            {
                if(CheckUser.Email == userModel.Email)
                return new Response() { Sataus = StatusResponse.Failed, Message = " This User Alreday Registerd" };
            }

            var user = new ApplicationUser()
            {
                UserName = userModel.Name,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = userModel.Email

            };

            var result = await UserManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded)
            {
                return new Response() { Sataus = StatusResponse.Failed, Message = " Failed To Register" };

            }

            return new Response() { Sataus = StatusResponse.Success, Message = " User SucessFully Created" };
        }


        public async Task<JwtSecurityToken> Login(LoginModel model)
        {
            var user = await UserManager.FindByNameAsync(model.Name);

            if (user != null && await UserManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await UserManager.GetRolesAsync(user);

                var authClims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name , user.UserName),
                    new Claim(ClaimTypes.NameIdentifier , user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())

                };


                foreach (var role in userRoles)
                {
                    authClims.Add(
                        new Claim(ClaimTypes.Role, role)
                        );
                }

                var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));


                var token = new JwtSecurityToken(

                    issuer: Configuration["JWT:ValidIssuer"],
                    audience: Configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClims,
                    signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
                    );

                return token;

            }

            return null;

        }




    }
}
