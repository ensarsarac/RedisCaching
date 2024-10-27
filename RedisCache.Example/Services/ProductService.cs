using Microsoft.EntityFrameworkCore;
using RedisCache.Example.DataAccess.Context;
using RedisCache.Example.DataAccess.Entity;

namespace RedisCache.Example.Services
{
	public class ProductService : IProductService
	{
		private readonly RedisContext _redisContext;

		public ProductService(RedisContext redisContext)
		{
			_redisContext = redisContext;
		}

		public async Task CreateProduct(Product product)
		{
			await _redisContext.Products.AddAsync(product);

			await _redisContext.SaveChangesAsync();
		}

		public async Task<List<Product>> GetAllProducts()
		{
			var values = await _redisContext.Products.ToListAsync();

			return values;
		}
	}
}
