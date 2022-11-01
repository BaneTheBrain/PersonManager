using PersonManagerService.Application.Abstractions;
using PersonManagerService.Domain.Models;
using PersonManagerService.Infrastructure.Contexts;

namespace PersonManagerService.Persistance.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonManagerServiceDbContext _personManagerServiceDbContext;

    public PersonRepository(PersonManagerServiceDbContext personManagerServiceDbContext) => _personManagerServiceDbContext = personManagerServiceDbContext ?? throw new ArgumentNullException(nameof(personManagerServiceDbContext));

    public void Create(Person person) => _personManagerServiceDbContext.Set<Person>().Add(person);
}
