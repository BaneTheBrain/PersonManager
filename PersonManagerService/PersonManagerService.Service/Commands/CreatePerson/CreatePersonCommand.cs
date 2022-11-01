using PersonManagerService.Application.Abstractions;
using PersonManagerService.Application.DTOs;

namespace PersonManagerService.Application.Commands.CreatePerson;

public sealed record CreatePersonCommand(string? FirstName,
    string? LastName,
    ICollection<string>? Skills,
    ICollection<PersonSocialMediaAccountDto>? SocialMediaAccounts) : ICommand<Guid>;
