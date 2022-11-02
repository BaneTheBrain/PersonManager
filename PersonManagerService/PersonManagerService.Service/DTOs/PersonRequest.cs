namespace PersonManagerService.Application.DTOs;

public sealed record PersonRequest(
    string FirstName,
    string LastName,
    IEnumerable<string>? PersonSkills,
    IEnumerable<PersonSocialMediaAccountRequest>? PersonSocialMediaAccounts);
