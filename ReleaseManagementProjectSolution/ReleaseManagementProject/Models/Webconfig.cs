using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Web.Http.Filters;
using System.Web.Http;
using AuthenticationService.Models;
using AuthenticationService.Managers;

namespace ReleaseManagementProject.Models
{
    public class MyGlobalActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes<SkipMyGlobalActionFilterAttribute>().Any())
            {
                return;
            }
            if (!filterContext.Request.Headers.Contains("JWT_TOKEN"))
                throw new UnauthorizedAccessException();
            else
            {
                String token = filterContext.Request.Headers.GetValues("JWT_TOKEN").First();
                IAuthContainerModel model = new JWTContainerModel();
                IAuthService authService = new JWTService(model.SecretKey);
                if (!authService.IsTokenValid(token))
                    throw new UnauthorizedAccessException();
                else
                {
                    List<Claim> claims = authService.GetTokenClaims(token).ToList();

                    Console.WriteLine(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name)).Value);
                    Console.WriteLine(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Role)).Value);
                   // Console.WriteLine(claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.PrimarySid)).Value);
                }
            }
        }
    }

    public class SkipMyGlobalActionFilterAttribute : Attribute
    {
    }
}