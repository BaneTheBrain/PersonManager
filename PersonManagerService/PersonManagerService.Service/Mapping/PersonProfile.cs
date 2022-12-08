using AutoMapper;
using PersonManagerService.Application.Commands.CreatePerson;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Application.Helpers;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Domain.Mapping;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreateOrUpdatePersonCommand, Person>()
            .ForMember(p => p.PersonSkills, o => o.MapFrom(cmd => MapSocialSkills(cmd)))
            .ForMember(p => p.PersonSocialMediaAccounts, o => o.MapFrom(cmd => MapSocialMediaAccounts(cmd)));

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

    private IEnumerable<PersonSkill> MapSocialSkills(CreateOrUpdatePersonCommand command)
    {
        if (command.Skills is null)
        {
            return Enumerable.Empty<PersonSkill>();
        }

        return command.Skills.Select(socialSkill =>
            new PersonSkill
            {
                Name = socialSkill
            });
    }

    private IEnumerable<PersonSocialMediaAccount> MapSocialMediaAccounts(CreateOrUpdatePersonCommand command)
    {
        if (command.SocialMediaAccounts is null)
        {
            return Enumerable.Empty<PersonSocialMediaAccount>();
        }

        return command.SocialMediaAccounts.Select(socialMediaAccount =>
            {
                var acc = new PersonSocialMediaAccount
                {
                    Address = socialMediaAccount.Address,
                    SocialMediaAccountId = socialMediaAccount.AccountId,
                    SocialMediaAccount = socialMediaAccount.AccountId == Guid.Empty ? new SocialMediaAccount { Type = socialMediaAccount.Type } : null
                };
                return acc;
               }
            );
    } 

    #endregion
}
