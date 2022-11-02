namespace PersonManagerService.Domain.DTOs;

public sealed record PersonSocialMediaAccountRequest(Guid AccountId, string Address, string Type);
