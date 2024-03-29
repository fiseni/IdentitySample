﻿namespace IdentitySample.Identity.Api.Models;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;

    public List<RoleNameDto> Roles { get; set; } = new();
}
