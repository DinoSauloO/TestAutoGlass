using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAutoGlass.API.Controllers;
using TestAutoGlass.Domain.Entities;
using TestAutoGlass.Domain.Interfaces.Services;
using TestAutoGlass.Domain.Requests;
using TestAutoGlass.Domain.Requests.Create;
using TestAutoGlass.Domain.Requests.Update;
using TestAutoGlass.Domain.Responses;
using Xunit;

namespace TestAutoGlass.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductsService> _productsServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _productsServiceMock = new Mock<IProductsService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new ProductsController(_productsServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            int productId = 1;
            var product = new Products { Id = productId };
            var response = new ProductsResponse { Id = productId };
            _productsServiceMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);
            _mapperMock.Setup(x => x.Map<ProductsResponse>(product)).Returns(response);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var prodResponse = Assert.IsType<ProductsResponse>(okResult.Value);
            Assert.Equal(productId, prodResponse.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;
            Products product = null;
            _productsServiceMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAllFiltered_ReturnsOkResult_WithProductsResponseList()
        {
            // Arrange
            var productsParams = new ProductsRequest();
            int pageNumber = 1;
            int pageQuantity = 10;
            var products = new List<Products> { new Products { Id = 1 }, new Products { Id = 2 } };
            var response = new List<ProductsResponse> { new ProductsResponse { Id = 1 }, new ProductsResponse { Id = 2 } };
            _productsServiceMock.Setup(x => x.GetAllAsync(productsParams, pageNumber, pageQuantity)).ReturnsAsync(products);
            _mapperMock.Setup(x => x.Map<IEnumerable<ProductsResponse>>(products)).Returns(response);

            // Act
            var result = await _controller.GetAllFiltered(productsParams, pageNumber, pageQuantity);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var prodResponseList = Assert.IsType<List<ProductsResponse>>(okResult.Value);
            Assert.Equal(2, prodResponseList.Count);
        }

        [Fact]
        public async Task GetAllFiltered_ReturnsNotFound_WhenNoProductsFound()
        {
            // Arrange
            var productsParams = new ProductsRequest();
            int pageNumber = 1;
            int pageQuantity = 10;
            var products = new List<Products>();
            _productsServiceMock.Setup(x => x.GetAllAsync(productsParams, pageNumber, pageQuantity)).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllFiltered(productsParams, pageNumber, pageQuantity);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Insert_ReturnsOkResult_WithCreatedProductResponse()
        {
            // Arrange
            var createProductRequest = new CreateProductRequest();
            var product = new Products();
            var response = new ProductsResponse();
            _mapperMock.Setup(x => x.Map<Products>(createProductRequest)).Returns(product);
            _productsServiceMock.Setup(x => x.CreateAsync(product)).ReturnsAsync(product);
            _mapperMock.Setup(x => x.Map<ProductsResponse>(product)).Returns(response);

            // Act
            var result = await _controller.Insert(createProductRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var prodResponse = Assert.IsType<ProductsResponse>(okResult.Value);
            Assert.Equal(response, prodResponse);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WithUpdatedProductResponse()
        {
            // Arrange
            var updateProductRequest = new UpdateProductRequest();
            var product = new Products();
            var response = new ProductsResponse();
            _mapperMock.Setup(x => x.Map<Products>(updateProductRequest)).Returns(product);
            _productsServiceMock.Setup(x => x.UpdateAsync(product)).ReturnsAsync(product);
            _mapperMock.Setup(x => x.Map<ProductsResponse>(product)).Returns(response);

            // Act
            var result = await _controller.Update(updateProductRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var prodResponse = Assert.IsType<ProductsResponse>(okResult.Value);
            Assert.Equal(response, prodResponse);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var updateProductRequest = new UpdateProductRequest();
            Products product = null;
            _productsServiceMock.Setup(x => x.UpdateAsync(product)).ReturnsAsync(product);

            // Act
            var result = await _controller.Update(updateProductRequest);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            int id = 1;
            _productsServiceMock.Setup(s => s.DeleteAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            int id = 1;
            _productsServiceMock.Setup(s => s.DeleteAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }




    }
}
