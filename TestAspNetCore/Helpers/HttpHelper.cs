using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TestAspNetCore.Helpers
{
    public class HttpHelper
    {
        public static async Task SetTokenCookie(HttpContext context, string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            context.Response.Cookies.Append("refreshToken", token, cookieOptions);
            await Task.FromResult(0);
        }

        public static async Task<string> GetIP4Address(HttpContext context)
        {
            string result = string.Empty;
            if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
                result = context.Request.Headers["X-Forwarded-For"];
            else
                result = context.Connection.RemoteIpAddress.MapToIPv4().ToString();

            return await Task.FromResult(result);
        }
    }
}
