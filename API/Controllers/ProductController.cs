using BL.AppServices;
using BL.DTOs;
using BL.Helper;
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
    public class ProductController : ControllerBase
    {
        private readonly ProductAppService ProductAppService;

        public ProductController(ProductAppService productAppService)
        {
            this.ProductAppService = productAppService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ProductAppService.GetAllProduct());
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(ProductAppService.GetProduct(id));
        }
        [HttpGet]
        [Route("productsincategroy/{categroyId}")]
        public IActionResult GetAllProductsInCategroy(int categroyId)
        {
            return Ok(ProductAppService.GetAllProductsPerCategroy(categroyId));
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (ProductAppService.CheckProductIsExistByName(productDto))
                {
                    return BadRequest(new Response { Sataus = "ERORR", Message = "This Product Is Already Added" });
                }

                ProductDto product = ProductAppService.CreateNewProduct(productDto);

                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return BadRequest(new Response { Sataus = "ERROR", Message = "Faild To Create Product" });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (ProductAppService.UpdateProduct(productDto))
                {
                    return Ok(new Response { Sataus = "Updated", Message = "Product Updated Susscffuly" });
                }
                else
                {
                    return BadRequest(new Response { Sataus = "ERROR", Message = " Product Updated Failed" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (ProductAppService.GetProduct(id) == null)
                {
                    return BadRequest(new Response { Sataus = "ERROR", Message = "Product Is Already Deleted" });
                }

                var result = ProductAppService.DeleteProduct(id);
                if (result)
                {
                    return Ok(new Response { Sataus = "Deleted", Message = "Product Deleted Susscffuly" });
                }
                else
                {
                    return BadRequest(new Response { Sataus = "ERROR", Message = " Product Deleted Failed" });
                }

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{pageNumber}/{PageSize}")]
        public IActionResult GetProductByPage(int pageNumber, int PageSize)
        {
            return Ok(ProductAppService.GetProductByPage(pageNumber, PageSize));
        }

        [HttpGet()]
        [Route("categroy/{categroyId}/{pageNumber}/{PageSize}")]
        public IActionResult GetProductInCategroyByPage( int categroyId ,int pageNumber, int PageSize )
        {
            return Ok(ProductAppService.GetProductInCategroyByPage(categroyId, pageNumber, PageSize ));
        }
    }
}
