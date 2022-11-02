using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Infrastructure.Contexts;

namespace PersonManagerService.Persistance.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PersonManagerServiceDbContext _personManagerServiceDbContext;

    public UnitOfWork(PersonManagerServiceDbContext personManagerServiceDbContext) => _personManagerServiceDbContext = personManagerServiceDbContext ?? throw new ArgumentNullException(nameof(personManagerServiceDbContext));

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _personManagerServiceDbContext.SaveChangesAsync(cancellationToken);
    }
}
