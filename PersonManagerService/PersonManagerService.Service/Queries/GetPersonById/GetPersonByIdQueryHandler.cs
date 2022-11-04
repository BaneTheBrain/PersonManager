using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.Commands.CreatePerson;
using PersonManagerService.Domain.DTOs;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Domain.Queries.GetPerson;

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonResponse>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<GetPersonByIdQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetPersonByIdQueryHandler(IUnitOfWork uow, ILogger<GetPersonByIdQueryHandler> logger, IMapper mapper)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PersonResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"{nameof(GetPersonByIdQueryHandler)} -> Handle request initiated.");

            var dbPerson = await _uow.PersonRepository.GetPersonWithSocialMediaAccountsAndSkills(request.PersonId, cancellationToken);
            var person = _mapper.Map<Person, PersonResponse>(dbPerson);

            _logger.LogInformation($"{nameof(GetPersonByIdQueryHandler)} -> Handle request suceeded.");
            return person;
        }
        catch
        {
            _logger.LogError($"{nameof(GetPersonByIdQueryHandler)} -> Handle request failed.");
            throw;
        }
    }
}

