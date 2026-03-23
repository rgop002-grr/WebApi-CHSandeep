using BussinessLayer.Business;
using BussinessLayer.IBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.ModelDto;
using RepositoryLayer.Models;

namespace WebApplication2.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBussiness _bussiness;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductBussiness bussiness, ILogger<ProductController> logger)
        {
            _bussiness = bussiness;
            _logger = logger;
        }
        [HttpGet("ProductDetails")]
        public IActionResult GetProductDetails()
        {
            _logger.LogInformation("Fetching product details");

            try
            {
                var res = _bussiness.GetProducts();

                if (res == null || !res.Any())
                {
                    return NotFound("No products available");
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
