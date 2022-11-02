using Microsoft.EntityFrameworkCore;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.Models;
using PersonManagerService.Infrastructure.Contexts;
using System;

namespace PersonManagerService.Persistance.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonManagerServiceDbContext _personManagerServiceDbContext;

    public PersonRepository(PersonManagerServiceDbContext personManagerServiceDbContext) => _personManagerServiceDbContext = personManagerServiceDbContext ?? throw new ArgumentNullException(nameof(personManagerServiceDbContext));

    public void Create(Person person) => _personManagerServiceDbContext.Set<Person>().Add(person);

    public Task<Person> Get(Guid id, CancellationToken cancellationToken)
    {
        return _personManagerServiceDbContext.Set<Person>()
                .Include(p => p.PersonSocialMediaAccounts)
                .ThenInclude(psma => psma.SocialMediaAccount)
                .Include(p => p.PersonSkills)
                .FirstOrDefaultAsync(x => x.PersonId == id, cancellationToken);
    }

    public async Task<IEnumerable<Person>> Get(CancellationToken cancellationToken)
    {
        return await _personManagerServiceDbContext.Set<Person>()
              .Include(p => p.PersonSocialMediaAccounts)
              .ThenInclude(psma => psma.SocialMediaAccount)
              .Include(p => p.PersonSkills)
              .ToListAsync();
    }
}
