using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Distributed.Caching.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly IDistributedCache _distributedCache;

		public ValuesController(IDistributedCache distributedCache)
		{
			_distributedCache = distributedCache;
		}

		[HttpGet("setName/{name}/{surname}")]
		public async Task<IActionResult> Set(string name, string surname)
		{
			await _distributedCache.SetStringAsync("name", name);
			await _distributedCache.SetStringAsync("surname", surname);
			return Ok("Veri başarıyla cachlendi");
		}

		[HttpGet("getName")]
		public async Task<IActionResult> Get()
		{
			var name = await _distributedCache.GetStringAsync("name");
			var surname = await _distributedCache.GetStringAsync("surname");
			return Ok(new
			{
				name,
				surname
			});
		}

	}
}
