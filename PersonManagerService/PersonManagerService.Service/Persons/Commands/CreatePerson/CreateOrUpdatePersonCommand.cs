using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Abstractions;

namespace PersonManagerService.Application.Commands.CreatePerson;

public sealed record CreateOrUpdatePersonCommand(
    Guid PersonId,
    string FirstName,
    string LastName,
    IEnumerable<string>? Skills,
    IEnumerable<PersonSocialMediaAccountRequest>? SocialMediaAccounts) : ICommand<Guid>;
