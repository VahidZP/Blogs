using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Extensions
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Http;

    public static class HttpContextExtension
    {
        /*show Ip address for Extension*/
        public static string GetUserIp(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString() ?? "Is not Ip";
        }

        /*show UserName for Extension*/
        public static string GetClaim(this ClaimsPrincipal user, string claimType)
        {
            return user.Claims.SingleOrDefault(c => c.Type == claimType)?.Value ?? "Is not FullName";
        }

        /*show UserID for Extension*/
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return Convert.ToInt32(user.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
