namespace PersonManagerService.Domain.Abstractions;

public interface IUnitOfWork
{
    IPersonRepository PersonRepository { get; }
    ISocialMediaAccountRepository SocialMediaAccountRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
