using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.DTOs;
using PersonManagerService.Domain.Queries.GetPerson;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Domain.Queries.GetPersons;

public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, IEnumerable<PersonResponse>>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<GetPersonsQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetPersonsQueryHandler(IUnitOfWork uow, ILogger<GetPersonsQueryHandler> logger, IMapper mapper)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<PersonResponse>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"{nameof(GetPersonByIdQueryHandler)} -> Handle request initiated.");

            var persons = await _uow.PersonRepository.GetPersonWithSocialMediaAccountsAndSkills(cancellationToken);
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
