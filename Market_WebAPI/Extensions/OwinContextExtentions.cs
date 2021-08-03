using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Market_WebAPI.Extensions
{
    public static class OwinContextExtensions
    {
        public static string GetCPF(this IOwinContext ctx)
        {
            var result = "-1";
            var claim = ctx.Authentication.User.Claims.FirstOrDefault(c => c.Type == "CPF");
            if (claim != null)
            {
                result = claim.Value;
            }
            return result;
        }
    }
}