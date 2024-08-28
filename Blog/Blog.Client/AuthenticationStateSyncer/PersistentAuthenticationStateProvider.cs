using Blog.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Blog.Client.AuthenticationStateSyncer;

public class PersistentAuthenticationStateProvider(PersistentComponentState persistentState) : AuthenticationStateProvider
{
    private static readonly Task<AuthenticationState> _unauthenticatedTask =
        Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (!persistentState.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
        {
            return _unauthenticatedTask;
        }

        Claim[] claims =
        [
            new Claim(ClaimTypes.NameIdentifier, userInfo.UserId),
            new Claim(ClaimTypes.Name, userInfo.Name ?? string.Empty),
            new Claim(ClaimTypes.Email, userInfo.Email ?? string.Empty),
            new Claim(ClaimTypes.Role, userInfo.Role ?? string.Empty)
        ];

        return Task.FromResult(
        new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims,
            authenticationType: nameof(PersistentAuthenticationStateProvider)))));
    }
}