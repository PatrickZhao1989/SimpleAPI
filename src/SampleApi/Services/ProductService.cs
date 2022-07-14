using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleApi.Entities;
using SampleApi.Models;

namespace SampleApi.Services;

public interface IProductService
{
	Task<IEnumerable<ProductDto>> GetProducts();
	Task<ProductDto> AddProduct(ProductDto newProductDto);

	Task<ProductDto> UpdateProduct(ProductDto toBeUpdatedProductDto);
	Task RemoveProductById(int productId);
}

public class ProductService : IProductService
{

	private readonly SampleApiDbContext _dbContext;
	private readonly IMapper _mapper;

	public ProductService(
		SampleApiDbContext dbContext,
		IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<ProductDto> AddProduct(ProductDto newProductDto)
	{
		var productEntity = _mapper.Map<Product>(newProductDto);

		_dbContext.Products.Add(productEntity);
		await _dbContext.SaveChangesAsync();

		var newlyAddedId = productEntity.Id;

		var newlyAddedProduct = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == newlyAddedId);

		if (newlyAddedProduct == null)
		{
			throw new ArgumentException("Newly added product cannot be found");
		}

		return _mapper.Map<ProductDto>(newlyAddedProduct);
	}

	public async Task<IEnumerable<ProductDto>> GetProducts()
	{
		var result = await _dbContext.Products.ToArrayAsync();
		return _mapper.Map<IEnumerable<ProductDto>>(result);
	}

	public async Task RemoveProductById(int productId)
	{
		var toBeDeletedProductEntity = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);

		if (toBeDeletedProductEntity == null)
		{
			throw new ArgumentException($"No product with Id {productId} can be found");
		}

		_dbContext.Products.Remove(toBeDeletedProductEntity);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<ProductDto> UpdateProduct(ProductDto toBeUpdatedProductDto)
	{
		// TODO Move validation to it's own layer/filter, use things like FluentValidation
		if (toBeUpdatedProductDto.Id < 0)
		{
			throw new ArgumentException("");
		}

		var existingEntity = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == toBeUpdatedProductDto.Id);

		if (existingEntity == null)
		{
			throw new ArgumentException($"No product with Id {toBeUpdatedProductDto.Id} can be found");
		}

		existingEntity.Description = toBeUpdatedProductDto.Description;
		existingEntity.Name = toBeUpdatedProductDto.Name;
		existingEntity.Price = toBeUpdatedProductDto.Price;

		await _dbContext.SaveChangesAsync();

		var updatedEntity = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == toBeUpdatedProductDto.Id);
		// TODO Validate the successful updated properties
		
		return _mapper.Map<ProductDto>(updatedEntity);
	}
}