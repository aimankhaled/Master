using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Helpers
{
    public static class IdentityHelper
    {
        public class Roles
        {
            public const string Admin = "Admin";
        }
        public class Claims
        {
            public const string EmpNumber = "EN";
            public const string Name = "Name";
        }
        public class Policies
        {
            public const string RequireAdmin = "RequireAdmin";

            public Policies(AuthorizationOptions options)
            {
                options.AddPolicy(RequireAdmin, x => x.RequireAssertion(AdminPolicyHandler));

            }
            private static bool AdminPolicyHandler(AuthorizationHandlerContext arg)
            {
                return arg.User.Claims.FirstOrDefault(claim =>
                          claim.Type == Claims.EmpNumber && !string.IsNullOrEmpty(claim.Value)) != null;
            }
           
        }

        public static int GetNumber(this ClaimsPrincipal user)
        {
                return int.Parse(user.Claims.FirstOrDefault(x => x.Type == Claims.EmpNumber).Value);

        }
    }
}
