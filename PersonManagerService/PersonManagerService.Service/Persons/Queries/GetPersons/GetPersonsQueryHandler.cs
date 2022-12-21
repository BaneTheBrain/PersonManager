using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.Models;
using Polly.CircuitBreaker;
using PersonManagerService.Application.Abstractions;
using PersonManagerService.Application.DTOs;
using PPersonManagerService.Application.Queries.GetPerson;
using PersonManagerService.Domain.Mapping;
using System;
using PersonManagerService.Application.Helpers;

namespace PersonManagerService.Application.Queries.GetPersons;

public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, IEnumerable<PersonResponse>>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<GetPersonsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IResilientService _resilientService;


    public GetPersonsQueryHandler(IResilientService resilientService, IUnitOfWork uow, ILogger<GetPersonsQueryHandler> logger, IMapper mapper)
    {
        _resilientService = resilientService ?? throw new ArgumentNullException(nameof(resilientService));
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<PersonResponse>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"{nameof(GetPersonsQueryHandler)} -> Handle request initiated.");

            if (_resilientService.CircutBreakerPolicy.CircuitState == CircuitState.Open)
            {
                throw new Exception("Person repository unavailable.");
            }

            //var persons = await _resilientService.ResilientPolicy.ExecuteAsync(() => _uow.PersonRepository.GetPersonWithSocialMediaAccountsAndSkills(cancellationToken));
            //var retVal = persons.Select(person => _mapper.Map<Person, PersonResponse>(person));

            var people = await _uow.PersonRepository.GetPeople(cancellationToken);
            var retVal2 = Map(people);

            _logger.LogInformation($"{nameof(GetPersonsQueryHandler)} -> Handle request suceeded.");
            return retVal2;
        }
        catch
        {
            _logger.LogError($"{nameof(GetPersonsQueryHandler)} -> Handle request failed.");
            throw;
        }
    }

    private IEnumerable<PersonResponse> Map(IEnumerable<PersonSkillAccount> people)
    {
        var responses = new List<PersonResponse>();

        //to do: refactor it
        return people.GroupBy(x => x.PersonId).Select(g => 
            new PersonResponse(
                g.Key,
                g.First().FirstName,
                g.First().LastName,
                $"{g.First().FirstName} {g.First().LastName}".Trim().GetVovelsNumber(),
                $"{g.First().FirstName} {g.First().LastName}".Trim().GetConstenantsNumber(),
                $"{g.First().FirstName} {g.First().LastName}".Trim(),
                $"{g.First().FirstName} {g.First().LastName}".Trim().GetReversed(),
                g.Select(x => x.SkillName).Distinct(),
                g.Select(y => new PersonSocialMediaAccountResponse(y.AccountAddress, y.AccountType, y.AccountId.HasValue ? y.AccountId.Value : Guid.Empty)).Distinct()
                )
        );
    }
}
