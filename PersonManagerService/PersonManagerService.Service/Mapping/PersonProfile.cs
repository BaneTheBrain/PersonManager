using AutoMapper;
using PersonManagerService.Application.Commands.CreatePerson;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Application.Mapping;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonCommand, Person>()
            .ForMember(p => p.PersonSkills, o => o.MapFrom(personCommand => MapSocialSkills(personCommand)))
            .ForMember(p => p.PersonSocialMediaAccounts, o => o.MapFrom(pdto => MapSocialMediaAccounts(pdto)));

        CreateMap<Person, PersonDto>()
            .ForCtorParam(nameof(PersonDto.PersonSkills), o => o.MapFrom(person => person.PersonSkills.Select(x => x.Name)))
            .ForCtorParam(nameof(PersonDto.PersonSocialMediaAccounts), o => o.MapFrom(person => MapSocialMediaAccounts(person)));
       
        //.ForMember(p => p.PersonSkills, o => o.MapFrom(person => person.PersonSkills.Select(x => x.Name)))
        //.ForMember(p => p.PersonSocialMediaAccounts, o => o.MapFrom(person => MapSocialMediaAccounts(person)));

    }

    private List<PersonSocialMediaAccount> MapSocialMediaAccounts(CreatePersonCommand createPersonCommand)
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        return createPersonCommand.SocialMediaAccounts.Select(socialMediaAccount => new PersonSocialMediaAccount
        {
            Address = socialMediaAccount.Address,
            SocialMediaAccountId = socialMediaAccount.AccountId,
            SocialMediaAccount = socialMediaAccount.AccountId == Guid.Empty ?
                                                                new SocialMediaAccount { Type = socialMediaAccount.Type } : 
                                                                null
        }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.
    }

    private List<PersonSocialMediaAccountDto> MapSocialMediaAccounts(Person person) => person.PersonSocialMediaAccounts.Select(psma => new PersonSocialMediaAccountDto(psma.SocialMediaAccountId, psma.Address, psma.SocialMediaAccount.Type)).ToList();

    private List<PersonSkill> MapSocialSkills(CreatePersonCommand createPersonCommand) => createPersonCommand.Skills.Select(socialSkill => new PersonSkill
    {
        Name = socialSkill,
    }).ToList();
}
