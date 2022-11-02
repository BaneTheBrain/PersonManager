using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonManagerService.Application.Abstractions;
using PersonManagerService.Application.Commands.CreatePerson;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Application.Queries.GetPerson;

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<CreatePersonCommandHandler> _logger;
    private readonly IMapper _mapper;

    public GetPersonByIdQueryHandler(IPersonRepository personRepository, ILogger<CreatePersonCommandHandler> logger, IMapper mapper)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PersonResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"[{nameof(GetPersonByIdQueryHandler)}] Getting a person from the repository initiated.");

            var dbPerson = await _personRepository.Get(request.PersonId, cancellationToken);
            var person = _mapper.Map<Person, PersonResponse>(dbPerson);

            _logger.LogInformation($"[{nameof(GetPersonByIdQueryHandler)}] Getting a person from the repository suceeded.");
            return person;
        }
        catch (Exception)
        {
            _logger.LogError($"[{nameof(GetPersonByIdQueryHandler)}]Getting a person from the repository failed.");
            throw;
        }
    }
}

