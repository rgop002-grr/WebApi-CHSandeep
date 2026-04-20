using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.IBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RepositoryLayer.ModelDto;
using WebApplication2.Controllers;
using Xunit;

namespace WebApi_CHSandeep.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductBussiness> _mockBusiness;
        private readonly Mock<ILogger<ProductController>> _mockLogger;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockBusiness = new Mock<IProductBussiness>();
            _mockLogger = new Mock<ILogger<ProductController>>();
            _controller = new ProductController(_mockBusiness.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetProductDetails_ReturnsOkWithProducts()
        {
            var products = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "P1" },
                new ProductDto { Id = 2, Name = "P2" }
            };

            _mockBusiness.Setup(b => b.GetProducts()).Returns(products);

            var result = _controller.GetProductDetails();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Same(products, ok.Value);
        }

        [Fact]
        public async Task AddingProdetails_ReturnsOkMessage_OnSuccess()
        {
            var dto = new ProductDto { Id = 1, Name = "P" };
            _mockBusiness.Setup(b => b.AddProductAsync(dto)).ReturnsAsync(true);

            var result = await _controller.AddingProdetails(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Records inserted sucessfully", ok.Value);
            _mockBusiness.Verify(b => b.AddProductAsync(dto), Times.Once);
        }

        [Fact]
        public async Task UpdateProDetails_ReturnsBadRequest_WhenProductIsNull()
        {
            var result = await _controller.UpdateProDetails(null);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid data", bad.Value);
        }

        [Fact]
        public async Task UpdateProDetails_ReturnsNotFound_WhenUpdateFails()
        {
            var dto = new ProductDto { Id = 1, Name = "P" };
            _mockBusiness.Setup(b => b.UpdateProductAsync(dto)).ReturnsAsync(false);

            var result = await _controller.UpdateProDetails(dto);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Product not found", notFound.Value);
            _mockBusiness.Verify(b => b.UpdateProductAsync(dto), Times.Once);
        }

        [Fact]
        public async Task UpdateProDetails_ReturnsOk_WhenUpdateSucceeds()
        {
            var dto = new ProductDto { Id = 1, Name = "P" };
            _mockBusiness.Setup(b => b.UpdateProductAsync(dto)).ReturnsAsync(true);

            var result = await _controller.UpdateProDetails(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Product updated successfully", ok.Value);
            _mockBusiness.Verify(b => b.UpdateProductAsync(dto), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsBadRequest_WhenIdInvalid()
        {
            var result = await _controller.DeleteProduct(0);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Id must be greater than zero", bad.Value);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFound_WhenDeleteFails()
        {
            _mockBusiness.Setup(b => b.DeleteProductAsync(5)).ReturnsAsync(false);

            var result = await _controller.DeleteProduct(5);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Product not found", notFound.Value);
            _mockBusiness.Verify(b => b.DeleteProductAsync(5), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsOk_WhenDeleteSucceeds()
        {
            _mockBusiness.Setup(b => b.DeleteProductAsync(3)).ReturnsAsync(true);

            var result = await _controller.DeleteProduct(3);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Deleted successfully", ok.Value);
            _mockBusiness.Verify(b => b.DeleteProductAsync(3), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_Returns500_OnException()
        {
            _mockBusiness.Setup(b => b.DeleteProductAsync(7)).ThrowsAsync(new Exception("boom"));

            var result = await _controller.DeleteProduct(7);

            var obj = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, obj.StatusCode);
            Assert.Contains("Internal server error", obj.Value.ToString());
            _mockBusiness.Verify(b => b.DeleteProductAsync(7), Times.Once);
        }

        [Fact]
        public async Task UploadExcel_ReturnsBadRequest_WhenBusinessReturnsNoFile()
        {
            var mockFile = new Mock<IFormFile>();
            _mockBusiness.Setup(b => b.UploadExcelAsync(It.IsAny<IFormFile>())).ReturnsAsync("No file provided");

            var result = await _controller.UploadExcel(mockFile.Object);

            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No file provided", bad.Value);
            _mockBusiness.Verify(b => b.UploadExcelAsync(mockFile.Object), Times.Once);
        }

        [Fact]
        public async Task UploadExcel_ReturnsOk_WhenBusinessReturnsSuccess()
        {
            var mockFile = new Mock<IFormFile>();
            _mockBusiness.Setup(b => b.UploadExcelAsync(It.IsAny<IFormFile>())).ReturnsAsync("Upload successful");

            var result = await _controller.UploadExcel(mockFile.Object);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Upload successful", ok.Value);
            _mockBusiness.Verify(b => b.UploadExcelAsync(mockFile.Object), Times.Once);
        }
    }
}