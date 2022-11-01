namespace PersonManagerService.Application.DTOs;

public sealed record PersonDto(
    string? FirstName,
    string? LastName,
    ICollection<string>? Skills,
    ICollection<PersonSocialMediaAccountDto>? SocialMediaAccounts);
