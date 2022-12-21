using PersonManagerService.Domain.Models;

namespace PersonManagerService.Domain.Abstractions;

public interface IPersonRepository : IBaseRepository<Person>
{
    Task<Person> GetPersonWithSocialMediaAccountsAndSkills(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Person>> GetPersonWithSocialMediaAccountsAndSkills(CancellationToken cancellationToken);
    Task<IEnumerable<PersonSkillAccount>> GetPeople(CancellationToken cancellationToken);
    Task UpdatePerson(Person person, CancellationToken cancellationToken);
}
