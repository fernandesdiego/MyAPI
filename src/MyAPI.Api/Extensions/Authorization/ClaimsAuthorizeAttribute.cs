using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyAPI.Api.Extensions.Authorization
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimFilterRequirements))
        {
            Arguments = new object[] { new Claim(type: claimName, claimValue) };
        }
    }
}
