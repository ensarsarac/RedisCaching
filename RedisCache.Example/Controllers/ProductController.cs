using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCache.Example.DataAccess.Entity;
using RedisCache.Example.Dtos;
using RedisCache.Example.Services;

namespace RedisCache.Example.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductDto product)
		{
			await _productService.CreateProduct(new Product
			{
				Description = product.Description,
				Name = product.Name,
				Price = product.Price,
				Stock = product.Stock,
			});	
			return Ok("Success");
		}
		[HttpGet("GetAllProducts")]
		public async Task<IActionResult> GetAllProduct()
		{
			var response = await _productService.GetAllProducts();
			return Ok(response);
		}
	}
}
