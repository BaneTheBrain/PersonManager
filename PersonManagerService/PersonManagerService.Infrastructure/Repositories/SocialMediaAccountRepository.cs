using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.Models;
using PersonManagerService.Infrastructure.Contexts;
using PersonManagerService.Persistance.Repositories;

namespace PersonManagerService.Infrastructure.Repositories;

public class SocialMediaAccountRepository : BaseRepository<SocialMediaAccount>, ISocialMediaAccountRepository
{
    public SocialMediaAccountRepository(PersonManagerServiceDbContext personManagerServiceDbContext) : base(personManagerServiceDbContext)
    {
    }

    public IEnumerable<SocialMediaAccount> GetSocialMediaAccounts()
    {
        return GetAll();
    }
}
