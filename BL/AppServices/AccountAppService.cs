using BL.Bases;
using BL.Helper;
using BL.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class AccountAppService : BaseAppService
    {
        public AccountAppService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        public async Task<Response> Register(RegisterModel userModel)
        {
            var result = await TheUnitOfWork.AccountRepositroy.Register(userModel);
            return result;
        }

        public async Task<JwtSecurityToken> Login(LoginModel loginModel)
        {
            var result = await TheUnitOfWork.AccountRepositroy.Login(loginModel);
            return result;
        }
    }
}
