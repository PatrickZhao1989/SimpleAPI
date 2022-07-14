using Microsoft.AspNetCore.Mvc;
using SampleApi.Entities;
using SampleApi.Models;
using SampleApi.Services;

namespace SampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
	private readonly ILogger<ProductController> _logger;
	private readonly SampleApiDbContext _dbContext;
	private readonly IProductService _productService;


	public ProductController(
		ILogger<ProductController> logger,
		SampleApiDbContext dbContext,
		IProductService productService)
	{
		_logger = logger;
		_dbContext = dbContext;
		_productService = productService;
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var productArray = await _productService.GetProducts();
		return Ok(productArray);
	}

	[HttpPost]
	public async Task<IActionResult> AddSingle([FromBody] ProductDto productDto)
	{
		var addedProduct = await _productService.AddProduct(productDto);
		return Ok(addedProduct);
	}

	[HttpPut]
	public async Task<IActionResult> UpdateSingle([FromBody] ProductDto productDto)
	{
		var updatedProduct = await _productService.UpdateProduct(productDto);
		return Ok(updatedProduct);
	}

	[HttpDelete]
	public async Task<IActionResult> DeleteSingle(int productId)
	{

		await _productService.RemoveProductById(productId);
		return Ok("Product removed");
	}
}
