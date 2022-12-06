using IdentitySample.Identity.Api.Contracts;
using IdentitySample.Identity.Api.Models;
using IdentitySample.Identity.Setup.Authentication;
using IdentitySample.Identity.Setup.Authentication.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IdentitySample.Api.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ITokenHandler _tokenHandler;
    private readonly IUserService _userService;

    public AccountController(IAccountService accountService, ITokenHandler tokenHandler, IUserService userService)
    {
        _accountService = accountService;
        _tokenHandler = tokenHandler;
        _userService = userService;
    }

    [HttpPost("/identity/signup")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Sign up", Tags = new[] { "Identity" })]
    public async Task<ActionResult> Signup(SignUpDto signUpDto, CancellationToken cancellationToken = default)
    {
        await _accountService.SignUpAsync(signUpDto, cancellationToken);

        return Ok();
    }

    [HttpPost("/identity/signin")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Sign in", Tags = new[] { "Identity" })]
    public async Task<ActionResult<TokenResponse>> Signin(SignInDto signInDto, CancellationToken cancellationToken = default)
    {
        var user = await _accountService.SignInAsync(signInDto, cancellationToken);

        var token = await _tokenHandler.CreateTokensAsync(user);

        return Ok(token);
    }

    [HttpPost("/identity/refresh-token")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Refresh Token", Tags = new[] { "Identity" })]
    public async Task<ActionResult<TokenResponse>> RefreshToken(string refreshToken, CancellationToken cancellationToken = default)
    {
        var tokenEntry = await _tokenHandler.TakeRefreshTokenAsync(refreshToken);

        if (tokenEntry is null)
        {
            return BadRequest("Invalid refresh token.");
        }

        if (tokenEntry.RefreshToken.IsExpired())
        {
            return BadRequest("Expired refresh token.");
        }

        var user = await _userService.GetUserAsync(tokenEntry.Email, cancellationToken);
        if (user is null)
        {
            return BadRequest("Invalid user.");
        }

        var result = await _tokenHandler.CreateTokensAsync(user);

        return Ok(result);
    }

    [HttpPost("/identity/revoke-token")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Revoke Token", Tags = new[] { "Identity" })]
    public async Task<ActionResult> RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken = default)
    {
        await _tokenHandler.RevokeRefreshTokenAsync(refreshToken);

        return Ok();
    }
}
