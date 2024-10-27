using Microsoft.EntityFrameworkCore;
using RedisCache.Example.DataAccess.Entity;

namespace RedisCache.Example.DataAccess.Context
{
	public class RedisContext : DbContext
	{
		public RedisContext(DbContextOptions<RedisContext> context) : base(context)
		{

		}
		public DbSet<Product> Products{ get; set; }
    }
}
