using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Abstractions;

namespace PersonManagerService.Application.Queries.GetSocialMediaAccount;

public sealed record GetSocialMediaAccountsQuery() : IQuery<IEnumerable<SocialMediaAccountResponse>>;

