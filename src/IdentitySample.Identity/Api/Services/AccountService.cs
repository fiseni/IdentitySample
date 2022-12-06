using AutoMapper;
using IdentitySample.Identity.Api.Contracts;
using IdentitySample.Identity.Api.Models;
using IdentitySample.Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace IdentitySample.Identity.Api.Services;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountService(IMapper mapper,
                          UserManager<User> userManager,
                          SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<UserDto> SignInAsync(SignInDto signInDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(signInDto);

        var user = await _userManager.FindByEmailAsync(signInDto.Email);

        if (user is null) throw new Exception("Invalid email!");

        var result = await _signInManager.CheckPasswordSignInAsync(user, signInDto.Password, false);

        if (result.Succeeded is false)
        {
            throw new Exception(result.ToString());
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> SignUpAsync(SignUpDto signUpDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(signUpDto);

        var user = await _userManager.FindByEmailAsync(signUpDto.Email);

        if (user is not null) throw new Exception("The email is taken!");

        var newUser = _mapper.Map<User>(signUpDto);

        var result = await _userManager.CreateAsync(newUser, signUpDto.Password);

        if (result.Succeeded is false)
        {
            var error = string.Join(" ", result.Errors.Select(x => x.Description));
            throw new Exception(error);
        }

        return _mapper.Map<UserDto>(user);
    }
}
