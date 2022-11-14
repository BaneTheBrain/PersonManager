using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.DTOs;
using PersonManagerService.Domain.Queries.GetPerson;
using PersonManagerService.Domain.Models;
using Polly.Retry;
using Polly;
using Polly.CircuitBreaker;
using Polly.Wrap;
using PersonManagerService.Application.Abstractions;

namespace PersonManagerService.Domain.Queries.GetPersons;

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
            _logger.LogInformation($"{nameof(GetPersonByIdQueryHandler)} -> Handle request initiated.");

            if (_resilientService.CircutBreakerPolicy.CircuitState == CircuitState.Open)
            {
                throw new Exception("Person repository unavailable.");
            }

            var persons = await _resilientService.ResilientPolicy.ExecuteAsync(() =>_uow.PersonRepository.GetPersonWithSocialMediaAccountsAndSkills(cancellationToken));
            var retVal = persons.Select(person => _mapper.Map<Person, PersonResponse>(person));

            _logger.LogInformation($"{nameof(GetPersonByIdQueryHandler)} -> Handle request suceeded.");
            return retVal;
        }
        catch
        {
            _logger.LogError($"{nameof(GetPersonByIdQueryHandler)} -> Handle request failed.");
            throw;
        }
    }
}
