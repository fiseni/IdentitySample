using AutoMapper;
using AutoMapper.QueryableExtensions;
using IdentitySample.Identity.Api.Contracts;
using IdentitySample.Identity.Api.Models;
using IdentitySample.Identity.Domain;
using IdentitySample.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IdentitySample.Identity.Api.Services;

public class RoleService : IRoleService
{
    private readonly AppIdentityDbContext _dbContext;
    private readonly IMapper _mapper;

    public RoleService(AppIdentityDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<RoleDto> GetRoleAsync(Guid id, CancellationToken cancellationToken)
    {
        var roleDto = await _dbContext.Roles
            .Where(x => x.Id == id)
            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (roleDto is null) throw new KeyNotFoundException();

        return roleDto;
    }

    public async Task<List<RoleDto>> GetRolesAsync(CancellationToken cancellationToken)
    {
        var roleDtos = await _dbContext.Roles
            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (roleDtos is null) throw new KeyNotFoundException();

        return roleDtos;
    }

    public async Task<List<RoleNameDto>> GetRoleNamesAsync(CancellationToken cancellationToken)
    {
        var roleDtos = await _dbContext.Roles
            .ProjectTo<RoleNameDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (roleDtos is null) throw new KeyNotFoundException();

        return roleDtos;
    }

    public async Task<RoleDto> CreateRoleAsync(RoleCreateDto roleCreateDto, CancellationToken cancellationToken)
    {
        var role = _mapper.Map<Role>(roleCreateDto);

        _dbContext.Roles.Add(role);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var roleDto = _mapper.Map<RoleDto>(role);

        return roleDto;
    }

    public async Task<RoleDto> UpdateRoleAsync(Guid id, RoleUpdateDto roleUpdateDto, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles
            .Where(x => x.Id == id)
            .Include(x => x.RolePermissions)
            .FirstOrDefaultAsync(cancellationToken);

        if (role is null) throw new KeyNotFoundException();

        _mapper.Map(roleUpdateDto, role);

        await _dbContext.SaveChangesAsync();

        var roleDto = _mapper.Map<RoleDto>(role);

        return roleDto;
    }

    public async Task DeleteRoleAsync(Guid id, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (role is null) throw new KeyNotFoundException();

        _dbContext.Roles.Remove(role);

        await _dbContext.SaveChangesAsync();
    }
}
