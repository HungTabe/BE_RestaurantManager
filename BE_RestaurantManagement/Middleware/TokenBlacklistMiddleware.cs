using Microsoft.Extensions.Caching.Memory;

namespace BE_RestaurantManagement.Middleware
{
    public class TokenBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public TokenBlacklistMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token) && _cache.TryGetValue(token, out _))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Token has been revoked");
                return;
            }

            await _next(context);
        }
    }
}
