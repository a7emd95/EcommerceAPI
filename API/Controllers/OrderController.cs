using BL.AppServices;
using BL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderAppService OrderAppService;
        private readonly IHttpContextAccessor HttpContextAccessor;
        public OrderController(OrderAppService orderAppService, IHttpContextAccessor httpContext)
        {
            this.OrderAppService = orderAppService;
            this.HttpContextAccessor = httpContext;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult Get()
        {
            var userId = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(OrderAppService.GetAllOrderForUser(userId));
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userId = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(OrderAppService.GetOrderForuserById(userId, id));
        }

        // POST api/<OrderController>
        [HttpPost]
        [Route("purchase")]
        public IActionResult Post()
        {
            var userId = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var order = OrderAppService.PurchaseOrderForUser(userId);
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

  
       

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult  Delete(int id)
        {
            try
            {
                OrderAppService.DeleteOrder(id);
                return Ok(new Response() { Sataus = StatusResponse.Success, Message = "Delted Sussfully" });
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
