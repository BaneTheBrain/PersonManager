namespace PersonManagerService.Application.DTOs;

public sealed record PersonRequest(
    Guid PersonId,
    string FirstName,
    string LastName,
    IEnumerable<string>? PersonSkills,
    IEnumerable<PersonSocialMediaAccountRequest>? PersonSocialMediaAccounts);
