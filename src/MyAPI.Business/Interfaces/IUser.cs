using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MyAPI.Business.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetuserEmail();
        string IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
