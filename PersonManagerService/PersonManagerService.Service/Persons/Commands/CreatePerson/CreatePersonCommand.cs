using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.DTOs;

namespace PersonManagerService.Domain.Commands.CreatePerson;

public sealed record CreatePersonCommand(
    string FirstName,
    string LastName,
    IEnumerable<string>? Skills,
    IEnumerable<PersonSocialMediaAccountRequest>? SocialMediaAccounts) : ICommand<Guid>;
