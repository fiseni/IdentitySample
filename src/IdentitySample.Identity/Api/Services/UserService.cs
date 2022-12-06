using AutoMapper;
using AutoMapper.QueryableExtensions;
using IdentitySample.Identity.Api.Contracts;
using IdentitySample.Identity.Api.Models;
using IdentitySample.Identity.Domain;
using IdentitySample.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IdentitySample.Identity.Api.Services;

public class UserService : IUserService
{
    private readonly AppIdentityDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(AppIdentityDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var userDto = await _dbContext.Users
            .Where(x => x.Id == id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (userDto is null) throw new KeyNotFoundException();

        return userDto;
    }

    public async Task<UserDto> GetUserAsync(string email, CancellationToken cancellationToken)
    {
        var userDto = await _dbContext.Users
            .Where(x => x.NormalizedEmail == email.Normalize().ToUpperInvariant())
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (userDto is null) throw new KeyNotFoundException();

        return userDto;
    }

    public async Task<List<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
    {
        var userDtos = await _dbContext.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (userDtos is null) throw new KeyNotFoundException();

        return userDtos;
    }

    public async Task<UserDto> CreateUserAsync(UserCreateDto userCreateDto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(userCreateDto);

        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var userDto = _mapper.Map<UserDto>(user);

        return userDto;
    }

    public async Task<UserDto> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == id)
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null) throw new KeyNotFoundException();

        _mapper.Map(userUpdateDto, user);

        await _dbContext.SaveChangesAsync();

        var userDto = _mapper.Map<UserDto>(user);

        return userDto;
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null) throw new KeyNotFoundException();

        _dbContext.Users.Remove(user);

        await _dbContext.SaveChangesAsync();
    }
}
