using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Infrastructure.Contexts;
using PersonManagerService.Infrastructure.Repositories;

namespace PersonManagerService.Persistance.Repositories;

public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private bool _disposed;
    private readonly PersonManagerServiceDbContext _dbContext;
    private IPersonRepository _personRepository;
    private ISocialMediaAccountRepository _socialMediaAccountRepository;

    public UnitOfWork(PersonManagerServiceDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IPersonRepository PersonRepository => _personRepository ?? (_personRepository = new PersonRepository(_dbContext));

    public ISocialMediaAccountRepository SocialMediaAccountRepository => _socialMediaAccountRepository ?? (_socialMediaAccountRepository = new SocialMediaAccountRepository(_dbContext));

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _dbContext.Dispose();
        }
        _disposed = true;
    }
}
