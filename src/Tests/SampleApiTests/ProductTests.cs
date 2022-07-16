using Xunit;
using SampleApi.Mappers;
using SampleApi.Services;
using SampleApi.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SampleApi.Models;
using System.Threading.Tasks;

namespace SampleApiTests;

public class ProductTests
{
    private readonly SampleApiDbContext _testDbContext;
    private readonly IMapper _testMapper;

    public ProductTests()
    {
        var dbOptionBuilder = new DbContextOptionsBuilder<SampleApiDbContext>();
        dbOptionBuilder.UseInMemoryDatabase("TestDb");
        _testDbContext = new SampleApiDbContext(dbOptionBuilder.Options);


        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProductProfile()); 
        });
        _testMapper = mapperConfiguration.CreateMapper();
    }

    [Fact]
    public async Task Should_Add_1_ProductAsync()
    {
        // Arrange 
        var productService = new ProductService(_testDbContext, _testMapper);
        // Act 
        var addedProduct = await productService.AddProduct(new ProductDto { 
            Name = "TestProduct1",
            Description = "test p 1",
            Price = 1
            
        });

        // Assert
        Assert.Equal(addedProduct.Name, "TestProduct1");

    }
}