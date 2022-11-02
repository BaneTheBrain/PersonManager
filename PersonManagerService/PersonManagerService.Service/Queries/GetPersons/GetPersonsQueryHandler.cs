using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonManagerService.Application.Abstractions;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Application.Queries.GetPerson;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Application.Queries.GetPersons;

public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, IEnumerable<PersonDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<GetPersonsQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetPersonsQueryHandler(IPersonRepository personRepository, ILogger<GetPersonsQueryHandler> logger, IMapper mapper)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<PersonDto>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"[{nameof(GetPersonsQueryHandler)}] Getting a persons from the repository initiated.");

            var persons = await _personRepository.Get(cancellationToken);
            var retVal = persons?.Select(person => _mapper.Map<Person, PersonDto>(person));

            _logger.LogInformation($"[{nameof(GetPersonsQueryHandler)}] Getting a persons from the repository suceeded.");
            return retVal;
        }
        catch (Exception)
        {
            _logger.LogError($"[{nameof(GetPersonsQueryHandler)}]Getting a persons from the repository failed.");
            throw;
        }
    }
}
