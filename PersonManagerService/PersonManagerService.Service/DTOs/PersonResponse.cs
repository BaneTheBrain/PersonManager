namespace PersonManagerService.Application.DTOs;

public sealed record PersonResponse(
    Guid PersonId,
    string FirstName,
    string LastName,
    int Vovels,
    int Constenants,
    string FullName,
    string ReverseName,
    IEnumerable<string> PersonSkills,
    IEnumerable<PersonSocialMediaAccountResponse> PersonSocialMediaAccounts);
