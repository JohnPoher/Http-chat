using System.Linq;
using ChatServer.Domain.Entities;
using ChatServer.Extensions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ChatServer.Services.JwtToken;
using System.Windows.Input;

namespace ChatServer.Commands;

public abstract class AuthorizationCommand : ICommand
{
    private const string AuthorizationHeaderKey = "Authorization";

    public abstract string Path { get; }
    public abstract HttpMethod Method { get; }
    public abstract UserRole[] AllowedUserRoles { get; }

    private readonly IJwtTokenService _jwtTokenService;

    protected AuthorizationCommand(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    public async Task HandleRequestAsync(HttpListenerContext context)
    {
        var checkTokenResult = _jwtTokenService.CheckToken(context.Request.Headers[AuthorizationHeaderKey]);
        if (checkTokenResult.IsFaulted)
        {
            await context.WriteResponseAsync(401, "Unauthorized").ConfigureAwait(false);
            return;
        }

        if (!AllowedUserRoles.Contains(checkTokenResult.UserRole))
        {
            await context.WriteResponseAsync(403, "Forbidden").ConfigureAwait(false);
            return;
        }

        await HandleRequestInternalAsync(context).ConfigureAwait(false);
    }

    protected abstract Task HandleRequestInternalAsync(HttpListenerContext context);
}