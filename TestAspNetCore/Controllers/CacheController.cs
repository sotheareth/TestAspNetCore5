using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAspNetCore_Core.Dtos.Requests;
using TestAspNetCore_Core.Interfaces;

namespace TestAspNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetCacheValue([FromRoute] string key)
        {
            var value = await _cacheService.GetCacheValueAsync(key);
            return string.IsNullOrEmpty(value) ? (IActionResult) NotFound() : Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> SetCacheValue([FromBody] NewCacheRequestDto request)
        {
            await _cacheService.SetCacheValueAsync(request.Key, request.Value);
            return Ok();
        }
    }
}
