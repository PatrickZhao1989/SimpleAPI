using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SampleApi.Entities;
using SampleApi.Models;

namespace SampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
	private readonly ILogger<ProductController> _logger;
	private readonly SampleApiDbContext _dbContext;

	public ProductController(ILogger<ProductController> logger, SampleApiDbContext dbContext)
	{
		_logger = logger;
		_dbContext = dbContext;
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var result = await _dbContext.Products.ToArrayAsync();
		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> AddSingle([FromBody] ProductDto productDto)
	{
		var product = new Product
		{
			Name = productDto.Name,
			Description = productDto.Description,
			Price = productDto.Price
		};


		_dbContext.Products.Add(product);
		await _dbContext.SaveChangesAsync();
		var newlyAddedId = product.Id;

		var newlyAddedProduct = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == newlyAddedId);

		if (newlyAddedProduct == null)
		{
			throw new ArgumentException("No newly added product can be found");
		}
		var newlyAddedDto = new ProductDto
		{
			Id = newlyAddedProduct.Id,
			Description = newlyAddedProduct.Description,
			Name = newlyAddedProduct.Name,
			Price = newlyAddedProduct.Price
		};
		return Ok(newlyAddedDto);
	}

	// [HttpPut]
	// public async Task<IActionResult> UpdateSingle([FromBody] ProductDto productDto)
	// {
	// 	var result = await _dbContext.Products.ToArrayAsync();
	// 	return Ok(result);
	// }

	[HttpDelete]
	public async Task<IActionResult> DeleteSingle(int productId)
	{
		var toBeDeletedProduct = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);

		if (toBeDeletedProduct == null)
		{
			throw new ArgumentException($"No product with Id {productId} can be found");
		}

		_dbContext.Products.Remove(toBeDeletedProduct);
		await _dbContext.SaveChangesAsync();

		return Ok("Product removed");
	}
}
