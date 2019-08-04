using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
namespace KeyCloak.Helper
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CanAccessApp>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanAccessApp requirement)
        {
            Claim ct = context.User.Claims.Where(c => c.Type == "realm_access").FirstOrDefault();
            if (null != ct && ct.Value.Contains("CanAccessMobileApp"))
            {
                context.Succeed(requirement);
            }
            else if (context.User.HasClaim("user_realm_roles", "CanAccessMobileApp"))
            {
                context.Succeed(requirement);
            }
            else
            {
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
