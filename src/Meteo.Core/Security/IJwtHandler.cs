using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Meteo.Core.Security
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId, string role, IEnumerable<Claim> claims);
    }
}