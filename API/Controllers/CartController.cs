using BL.AppServices;
using BL.Helper;
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
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartAppServices CartAppServices;
        private readonly IHttpContextAccessor HttpContextAccessor;

        public CartController(CartAppServices cartAppService, IHttpContextAccessor httpContext)
        {
            this.CartAppServices = cartAppService;
            this.HttpContextAccessor = httpContext;
        }

        // GET: api/<CartController>
        [HttpGet]
        [Route("allProduct/{userId}")]
        public IActionResult GetAllProductInCart(string userId)
        {
            return Ok(CartAppServices.GetAllProductsInCart(userId));
        }



        // POST api/<CartController>
        [HttpPost]
        public IActionResult Post()
        {
            var userId = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                if (CartAppServices.CreateCart(userId) != null)
                    return Ok(new Response { Sataus = "Created", Message = "Cart is Created" });

                return BadRequest(new Response { Sataus = "Failed", Message = "Cart isnot Created" });
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("addProduct/{productId}/{quantity}")]
        public IActionResult AddProductToCart(int productId, int quantity)
        {


            try
            {
                 var userId = "c55eeb34-776d-4d1f-83ad-23db51a7725a";
               // var userId = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var productAdded = CartAppServices.AddnewProductToCart(userId, productId, quantity);
                if (productAdded != null)
                {
                    return Ok(productAdded);
                }

                return BadRequest(new Response { Sataus = "FAILED", Message = "Faild to add this product to cart" });

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }


        [HttpDelete]
        [Route("deleteProduct/{productId}")]
        public IActionResult DeleteProductInCart(int productId)
        {
            try
            {
                var userId = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                bool result = CartAppServices.DeleteProducatFromCart(userId, productId);
                if (result)
                    return Ok(new Response { Sataus = "Succcess", Message = " Product Deleted Succsessffly form Cart" });
                return BadRequest(new Response { Sataus = "Faild", Message = " Faild to delete Productform Cart" });

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }



        // PUT api/<CartController>/5
        [HttpPut]
        [Route("updateProduct/{productId}/{quantity}")]
        public IActionResult UpdateProducatInCart(int productId, int quantity)
        {
            try
            {
                var userId = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = CartAppServices.UpdateProducatFromCart(userId, productId, quantity);
                if (result)
                {
                    return Ok(new Response { Sataus = "Succcess", Message = " Product Updated Succsessffly" });
                }

                return BadRequest(new Response { Sataus = "FAILED", Message = "Faild to update this product " });

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // DELETE api/<CartController>/5
        [HttpDelete]
        public IActionResult Delete()
        {
            
            try
            {
                var userId = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                bool result = CartAppServices.DeleteCart(userId);
                if (result)
                    return Ok(new Response { Sataus = "Succcess", Message = "  Deleted Succsessffly  " });
                return BadRequest(new Response { Sataus = "Faild", Message = " Faild to delete  Cart" });

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
