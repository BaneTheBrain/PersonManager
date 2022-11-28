using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.DTOs;

namespace PersonManagerService.Domain.Queries.GetSocialMediaAccount;

public sealed record GetSocialMediaAccountsQuery() : IQuery<IEnumerable<SocialMediaAccountResponse>>;

