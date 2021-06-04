using BL.AppServices;
using BL.Helper;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountAppService AccountAppService;

        public AccountController(AccountAppService accountAppService)
        {
            this.AccountAppService = accountAppService;
        }

        [HttpPost]
        [Route("regiser")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await AccountAppService.Register(model);
                if (result.Sataus == StatusResponse.Success)
                    return Ok(result);
                return BadRequest(result);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }


        [HttpPost]
        [Route("regiserAdmin")]
        public async Task<IActionResult> RegisterForAdmin(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await AccountAppService.RegisterForAdmin(model);
                if (result.Sataus == StatusResponse.Success)
                    return Ok(result);
                return BadRequest(result);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await AccountAppService.Login(model);
                if (result != null)
                    return Ok(new
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(result)
                    }); ;

                return Unauthorized();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


    }
}
