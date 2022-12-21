using Microsoft.EntityFrameworkCore;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.Models;
using PersonManagerService.Infrastructure.Contexts;
using System;
using System.Collections.Generic;

namespace PersonManagerService.Persistance.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(PersonManagerServiceDbContext dbContext) : base(dbContext) { }

    public async Task UpdatePerson(Person person, CancellationToken cancellationToken)
    {
        var dbPerson = await GetPersonWithSocialMediaAccountsAndSkills(person.PersonId, cancellationToken);
        if (dbPerson is not null)
        {
            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.PersonSkills = person.PersonSkills;
            dbPerson.PersonSocialMediaAccounts = person.PersonSocialMediaAccounts;
        }
    }

    public async Task<Person> GetPersonWithSocialMediaAccountsAndSkills(Guid id, CancellationToken cancellationToken)
    {
        return await GetPeopleWithAssociatedReferences().FirstOrDefaultAsync(x => x.PersonId == id, cancellationToken);
    }

    public async Task<IEnumerable<Person>> GetPersonWithSocialMediaAccountsAndSkills(CancellationToken cancellationToken)
    {
        return await GetPeopleWithAssociatedReferences().ToListAsync();
    }

    public async Task<IEnumerable<PersonSkillAccount>> GetPeople(CancellationToken cancellationToken)
    {
       return await _dbContext.PersonSkillsAccounts.ToListAsync();
    }

    private IQueryable<Person> GetPeopleWithAssociatedReferences()
    {
        return _dbSet
             .Include(p => p.PersonSocialMediaAccounts)
             .ThenInclude(psma => psma.SocialMediaAccount)
             .Include(p => p.PersonSkills);
    }
}
