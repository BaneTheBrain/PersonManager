namespace PersonManagerService.Domain.DTOs;

public sealed record PersonSocialMediaAccountResponse(string Address, string Type, Guid SocialMediaAccountId);
