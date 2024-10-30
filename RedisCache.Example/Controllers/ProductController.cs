using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCache.Example.DataAccess.Entity;
using RedisCache.Example.Dtos;
using RedisCache.Example.RedisCache;
using RedisCache.Example.Services;
using System.Diagnostics;

namespace RedisCache.Example.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IRedisCacheService<IEnumerable<Product>> _redisCacheService;

		public ProductController(IProductService productService, IRedisCacheService<IEnumerable<Product>> redisCacheService)
		{
			_productService = productService;
			_redisCacheService = redisCacheService;
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
		[HttpGet("GetAllProductsWithRedis")]
		public async Task<IActionResult> GetAllProductsWithRedis()
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var redisCacheData = await _redisCacheService.GetAsync<IEnumerable<Product>>("products");

			if (redisCacheData is not null && redisCacheData.Any())
			{
				stopwatch.Stop();
				var redisDuration = stopwatch.ElapsedMilliseconds;
				Console.WriteLine("Redisten veri getirme süresi:"+redisDuration.ToString());
				return Ok(redisCacheData);
			}
			var values = await _productService.GetAllProducts();

			if(values == null || !values.Any()) return NotFound("Listelenecek ürün bulunamadı.");

			await _redisCacheService.SetAsync<IEnumerable<Product>>("products", values);
			
			return Ok(values);
		}

		[HttpGet("GetAllProductsWithDb")]
		public async Task<IActionResult> GetAllProductsWithDb()
		{

			var values = await _productService.GetAllProducts();

			if (values == null || !values.Any()) return NotFound("Listelenecek ürün bulunamadı.");


			return Ok(values);
		}
	}
}
