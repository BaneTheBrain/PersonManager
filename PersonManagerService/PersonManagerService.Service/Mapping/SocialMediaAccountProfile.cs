using AutoMapper;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Commands.CreatePerson;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Application.Mapping;

public class SocialMediaAccountProfile : Profile
{
	public SocialMediaAccountProfile()
	{
        CreateMap<SocialMediaAccount, SocialMediaAccountResponse>();
    }
}
