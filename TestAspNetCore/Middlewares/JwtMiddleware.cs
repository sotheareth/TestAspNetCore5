using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspNetCore.Helpers;
using TestAspNetCore.Options;
using TestAspNetCore_Core.Entities;
using TestAspNetCore_Core.Interfaces;

namespace TestAspNetCore.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSetting _setting;

        public JwtMiddleware(RequestDelegate next, JwtSetting setting)
        {
            _next = next;
            _setting = setting;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token != null)
                {
                    await attachUserToContext(context, userService, token);
                }
            }
            catch
            {
                //must return null then it stop pipeline
                return;
            }
            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                JwtSecurityToken jwtToken = await JwtHelper.ValidateToken(_setting, token);
                var claim = jwtToken.Claims.First(x => x.Type == "data");
                var userId = int.Parse(claim?.Value);

                // attach user to context on successful jwt validation
                context.Items["User"] = await userService.GetUser(userId);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
