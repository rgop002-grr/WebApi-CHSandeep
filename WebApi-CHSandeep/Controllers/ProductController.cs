using BussinessLayer.Business;
using BussinessLayer.IBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.ModelDto;
using RepositoryLayer.Models;

namespace WebApplication2.Controllers
{
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
            _logger.LogInformation("This method is getting Product details");
            try
            {
            List<ProductDto> res= _bussiness.GetProducts();
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
