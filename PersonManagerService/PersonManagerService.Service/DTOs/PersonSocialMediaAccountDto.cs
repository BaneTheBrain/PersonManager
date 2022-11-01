namespace PersonManagerService.Application.DTOs;

public sealed record PersonSocialMediaAccountDto(Guid AccountId, string? Address, string? Type);
