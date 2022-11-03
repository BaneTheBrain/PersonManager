using PersonManagerService.Domain.Models;

namespace PersonManagerService.Domain.Abstractions;

public interface IPersonRepository : IBaseRepository<Person>
{
    void Create(Person person);
    Task<Person> GetPersonWithSocialMediaAccountsAndSkills(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Person>> GetPersonWithSocialMediaAccountsAndSkills(CancellationToken cancellationToken);
}
