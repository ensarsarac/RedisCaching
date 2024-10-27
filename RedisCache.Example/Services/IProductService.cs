using RedisCache.Example.DataAccess.Entity;

namespace RedisCache.Example.Services
{
	public interface IProductService
	{
		Task CreateProduct(Product product);
		Task<List<Product>> GetAllProducts();
	}
}
