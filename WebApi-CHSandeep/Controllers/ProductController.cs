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

        [HttpPost("InsertingProDetails")]
        public async Task<IActionResult> AddingProdetails(ProductDto product)
        {
            try
            {
                var res =await _bussiness.AddProductAsync(product);
                return Ok("Records inserted sucessfully");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("UpdateProDetails")]
        public async Task<IActionResult> UpdateProDetails(ProductDto product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Invalid data");

                var result = await _bussiness.UpdateProductAsync(product);

                if (!result)
                    return NotFound("Product not found");

                return Ok("Product updated successfully");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id) // if using query param
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Id must be greater than zero");

                var deleted = await _bussiness.DeleteProductAsync(id);

                if (!deleted)
                    return NotFound("Product not found"); // important check

                return Ok("Deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            var result = await _bussiness.UploadExcelAsync(file);

            if (result.Contains("No file"))
                return BadRequest(result);

            return Ok(result);
        }
    }
}
