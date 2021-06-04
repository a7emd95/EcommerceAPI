using BL.AppServices;
using BL.DTOs;
using BL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategroyController : ControllerBase
    {
        private readonly CategoryAppService CategoryAppService;

        public CategroyController(CategoryAppService categoryAppService)
        {
            this.CategoryAppService = categoryAppService;
        }


        // GET: api/<CategroyController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(CategoryAppService.GetAllCategroies());
        }

        // GET api/<CategroyController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(CategoryAppService.GetCategroy(id));
        }
        [HttpGet]
        [Route("categroyWithProducts/{id}")]
        public IActionResult GetWithProduct(int id) {

            return Ok(CategoryAppService.GetCategoryWithProducts(id));
        
        }

        // POST api/<CategroyController>
        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public IActionResult Post(CategroyDto categroyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (CategoryAppService.CheckCategroyIsExistByName(categroyDto))
                {
                    return BadRequest(new Response { Sataus = StatusResponse.Failed , Message = "This Caregroy Is Already Added" });
                }

                CategroyDto categroy = CategoryAppService.CreateNewCategroy(categroyDto);
                if (categroy != null)
                {
                    return Ok(categroy);
                }
                return BadRequest(new Response { Sataus = StatusResponse.Failed, Message = "Faild To Create Categroy" });

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        // PUT api/<CategroyController>/5
        [Authorize(Roles = UserRole.Admin)]
        [HttpPut("{id}")]
        public IActionResult Put(CategroyDto categroyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = CategoryAppService.UpdateCategroy(categroyDto);
                if (result)
                {
                    return Ok(new Response { Sataus = StatusResponse.Success, Message = "Categroy Updated Susscffuly" });
                }
                else
                {
                    return BadRequest(new Response { Sataus = StatusResponse.Failed, Message = " Categroy Updated Failed" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message); ;
            }


        }

        // DELETE api/<CategroyController>/5
        [Authorize(Roles = UserRole.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            try
            {
                if (CategoryAppService.GetCategroy(id) == null)
                {
                    return BadRequest(new Response { Sataus = StatusResponse.Failed, Message = "Categroy Is Already Deleted" });
                }

                var result = CategoryAppService.DeleteCategroy(id);
                if (result)
                {
                    return Ok(new Response { Sataus = StatusResponse.Success, Message = "Categroy Deleted Susscffuly" });
                }
                else
                {
                    return BadRequest(new Response { Sataus = StatusResponse.Failed, Message = " Categroy Deleted Failed" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message); ;
            }
        }
    }
}
