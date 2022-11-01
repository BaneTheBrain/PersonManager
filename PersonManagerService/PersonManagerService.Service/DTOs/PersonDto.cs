namespace PersonManagerService.Application.DTOs;

public sealed record PersonDto(
    string? FirstName,
    string? LastName,
    ICollection<string>? PersonSkills,
    ICollection<PersonSocialMediaAccountDto>? PersonSocialMediaAccounts);
