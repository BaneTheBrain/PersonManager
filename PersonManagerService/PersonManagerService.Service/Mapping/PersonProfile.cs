using AutoMapper;
using PersonManagerService.Application.Helpers;
using PersonManagerService.Domain.Commands.CreatePerson;
using PersonManagerService.Domain.DTOs;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Domain.Mapping;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonCommand, Person>()
            .ForMember(p => p.PersonSkills, o => o.MapFrom(personCommand => MapSocialSkills(personCommand)))
            .ForMember(p => p.PersonSocialMediaAccounts, o => o.MapFrom(personCommand => MapSocialMediaAccounts(personCommand)));

        //mapping to record type
        CreateMap<Person, PersonResponse>()
            .ForCtorParam(nameof(PersonResponse.PersonSkills), o => o.MapFrom(person => person.PersonSkills.Select(x => x.Name)))
            .ForCtorParam(nameof(PersonResponse.PersonSocialMediaAccounts), o => o.MapFrom(person => MapSocialMediaAccounts(person)))
            .ForCtorParam(nameof(PersonResponse.Vovels), o => o.MapFrom(person => MapVovels(person)))
            .ForCtorParam(nameof(PersonResponse.Constenants), o => o.MapFrom(person => MapConstenants(person)))
            .ForCtorParam(nameof(PersonResponse.FullName), o => o.MapFrom(person => GetFullName(person)))
            .ForCtorParam(nameof(PersonResponse.ReverseName), o => o.MapFrom(person => MapReversedName(person)));
    }

    #region Private Methods

    private string GetFullName(Person person)
    {
        return $"{person.FirstName} {person.LastName}".Trim();
    }

    private int MapVovels(Person person)
    {
        return GetFullName(person).GetVovelsNumber();
    }
    private int MapConstenants(Person person)
    {
        return GetFullName(person).GetConstenantsNumber();
    }

    private string MapReversedName(Person person)
    {
        return GetFullName(person).GetReversed();
    }

    private IEnumerable<PersonSocialMediaAccountResponse> MapSocialMediaAccounts(Person person)
    {
        return person.PersonSocialMediaAccounts.Select(psma =>
            new PersonSocialMediaAccountResponse(psma.Address, psma.SocialMediaAccount.Type, psma.SocialMediaAccount.SocialMediaAccountId));
    }

    private IEnumerable<PersonSkill> MapSocialSkills(CreatePersonCommand createPersonCommand)
    {
        if (createPersonCommand.Skills is null)
        {
            return Enumerable.Empty<PersonSkill>();
        }

        return createPersonCommand.Skills.Select(socialSkill =>
            new PersonSkill
            {
                Name = socialSkill,
            });
    }

    private IEnumerable<PersonSocialMediaAccount> MapSocialMediaAccounts(CreatePersonCommand createPersonCommand)
    {
        if (createPersonCommand.SocialMediaAccounts is null)
        {
            return Enumerable.Empty<PersonSocialMediaAccount>();
        }

        return createPersonCommand.SocialMediaAccounts.Select(socialMediaAccount =>
            new PersonSocialMediaAccount
            {
                Address = socialMediaAccount.Address,
                SocialMediaAccountId = socialMediaAccount.AccountId,
                SocialMediaAccount = socialMediaAccount.AccountId == Guid.Empty ? new SocialMediaAccount { Type = socialMediaAccount.Type } : null
            });
    } 

    #endregion
}
