using PersonManagerService.Domain.Models;

namespace PersonManagerService.Domain.Abstractions;

public interface ISocialMediaAccountRepository : IBaseRepository<SocialMediaAccount>
{
    IEnumerable<SocialMediaAccount> GetSocialMediaAccounts();
}
