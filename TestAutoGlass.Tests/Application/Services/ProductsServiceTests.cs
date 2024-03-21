using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAutoGlass.Application.Services;
using TestAutoGlass.Domain.Entities;
using TestAutoGlass.Domain.Interfaces.Repositories;
using TestAutoGlass.Domain.Interfaces.Services;
using TestAutoGlass.Domain.Requests;
using Xunit;

namespace TestAutoGlass.Application.Tests.Services
{
    public class ProductsServiceTests
    {
        private readonly Mock<IProductsRepository> _productsRepositoryMock;
        private readonly IProductsService _productsService;

        public ProductsServiceTests()
        {
            _productsRepositoryMock = new Mock<IProductsRepository>();
            _productsService = new ProductsService(_productsRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenValidIdIsProvided()
        {
            // Arrange
            int productId = 1;
            var expectedProduct = new Products { Id = productId, Description = "Product 1" };
            _productsRepositoryMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await _productsService.GetByIdAsync(productId);

            // Assert
            Assert.Equal(expectedProduct, result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenInvalidIdIsProvided()
        {
            // Arrange
            int invalidProductId = -1;
            _productsRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidProductId)).ReturnsAsync((Products)null);

            // Act
            var result = await _productsService.GetByIdAsync(invalidProductId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfProducts_WhenValidParametersAreProvided()
        {
            // Arrange
            var productParams = new ProductsRequest();
            int pageNumber = 1;
            int pageQuantity = 10;
            var expectedProducts = new List<Products>
            {
                new Products { Id = 1, Description = "Product 1" },
                new Products { Id = 2, Description = "Product 2" },
                new Products { Id = 3, Description = "Product 3" }
            };
            _productsRepositoryMock.Setup(repo => repo.GetAllAsync(productParams, pageNumber, pageQuantity)).ReturnsAsync(expectedProducts);

            // Act
            var result = await _productsService.GetAllAsync(productParams, pageNumber, pageQuantity);

            // Assert
            Assert.Equal(expectedProducts, result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var validProductParams = new ProductsRequest();
            int pageNumber = 1;
            int pageQuantity = 10;
            var mockRepository = new Mock<IProductsRepository>();
            mockRepository.Setup(repo => repo.GetAllAsync(validProductParams, pageNumber, pageQuantity)).ReturnsAsync((IEnumerable<Products>)null);
            var productService = new ProductsService(mockRepository.Object);

            // Act
            var result = await productService.GetAllAsync(validProductParams, pageNumber, pageQuantity);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnProduct_WhenValidProductIsProvided()
        {
            // Arrange
            var product = new Products { Description = "Product 1" };
            var expectedProduct = new Products { Id = 1, Description = "Product 1" };
            _productsRepositoryMock.Setup(repo => repo.CreateAsync(product)).ReturnsAsync(expectedProduct);

            // Act
            var result = await _productsService.CreateAsync(product);

            // Assert
            Assert.Equal(expectedProduct, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnProduct_WhenValidProductIsProvided()
        {
            // Arrange
            var product = new Products { Id = 1, Description = "Product 1" };
            var expectedProduct = new Products { Id = 1, Description = "Product 1" };
            _productsRepositoryMock.Setup(repo => repo.UpdateAsync(product)).ReturnsAsync(expectedProduct);

            // Act
            var result = await _productsService.UpdateAsync(product);

            // Assert
            Assert.Equal(expectedProduct, result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var productToUpdate = new Products();
            var mockRepository = new Mock<IProductsRepository>();
            mockRepository.Setup(repo => repo.UpdateAsync(productToUpdate)).ReturnsAsync((Products)null);
            var productService = new ProductsService(mockRepository.Object);

            // Act
            var result = await productService.UpdateAsync(productToUpdate);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenValidIdIsProvided()
        {
            // Arrange
            int productId = 1;
            _productsRepositoryMock.Setup(repo => repo.SoftDeleteAsync(productId)).ReturnsAsync(true);

            // Act
            var result = await _productsService.DeleteAsync(productId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenInvalidIdIsProvided()
        {
            // Arrange
            int productId = 1;
            _productsRepositoryMock.Setup(repo => repo.SoftDeleteAsync(productId)).ReturnsAsync(false);

            // Act
            var result = await _productsService.DeleteAsync(productId);

            // Assert
            Assert.False(result);
        }
    }
}
