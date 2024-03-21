using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestAutoGlass.Domain.Entities;
using TestAutoGlass.Domain.Interfaces.Configuration;
using TestAutoGlass.Infra.Repositories;
using Xunit;

public class ProductsRepositoryTests
{
    private readonly Mock<IPostgreDbContext> _mockDbContext;
    private readonly ProductsRepository _repository;

    public ProductsRepositoryTests()
    {
        _mockDbContext = new Mock<IPostgreDbContext>();
        _repository = new ProductsRepository(_mockDbContext.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProduct_WhenProductExists()
    {
        // Arrange
        var expectedProduct = new Products { Id = 1 };
        var data = new List<Products> { expectedProduct };

        _mockDbContext.Setup(x => x.Products).ReturnsDbSet(data);

        // Act
        var product = await _repository.GetByIdAsync(1);

        // Assert
        Assert.Equal(expectedProduct, product);
    }

    [Fact]
    public async Task CreateAsync_ReturnsCreatedProduct()
    {
        // Arrange
        var newProduct = new Products { Id = 1 };
        var dbSetMock = new Mock<DbSet<Products>>();
        _mockDbContext.Setup(x => x.Products).Returns(dbSetMock.Object);

        // Act
        var product = await _repository.CreateAsync(newProduct);

        // Assert
        Assert.Equal(newProduct, product);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsUpdatedProduct_WhenProductExists()
    {
        // Arrange
        var existingProduct = new Products { Id = 1 };
        var data = new List<Products> { existingProduct };

        _mockDbContext.Setup(x => x.Products).ReturnsDbSet(data);

        // Act
        var product = await _repository.UpdateAsync(existingProduct);

        // Assert
        Assert.Equal(existingProduct, product);
        _mockDbContext.Verify(x => x.SaveChangesAsync(default(CancellationToken)), Times.Once);
    }

}
