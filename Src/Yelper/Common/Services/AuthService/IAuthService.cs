using Microsoft.Extensions.Configuration;

namespace Identity.Application.Common.Auth;

public interface IAuthService
{
    public string GenerateToken(Guid userId, string identifier, IConfiguration configuration);
}
