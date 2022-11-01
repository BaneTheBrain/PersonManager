using AutoMapper;
using PersonManagerService.Application.Commands.CreatePerson;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Application.Mapping;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonCommand, Person>()
            .ForMember(p => p.PersonSkills, o => o.MapFrom(personCommand => MapSocialSkills(personCommand)))
            .ForMember(p => p.PersonSocialMediaAccounts, o => o.MapFrom(pdto => MapSocialMediaAccounts(pdto)));
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

    private List<PersonSkill> MapSocialSkills(CreatePersonCommand createPersonCommand)
    {
        return createPersonCommand.Skills.Select(socialSkill => new PersonSkill
        {
            Name = socialSkill,
        }).ToList();
    }
}
