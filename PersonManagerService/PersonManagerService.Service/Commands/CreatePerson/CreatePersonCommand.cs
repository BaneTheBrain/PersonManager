using PersonManagerService.Application.Abstractions;
using PersonManagerService.Application.DTOs;

namespace PersonManagerService.Application.Commands.CreatePerson;

public sealed record CreatePersonCommand(
    string FirstName,
    string LastName,
    IEnumerable<string>? Skills,
    IEnumerable<PersonSocialMediaAccountRequest>? SocialMediaAccounts) : ICommand<Guid>;
