using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonManagerService.Application.Abstractions;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Application.Commands.CreatePerson;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreatePersonCommandHandler> _logger;


    public CreatePersonCommandHandler(IPersonRepository personRepository, IMapper mapper, IUnitOfWork unitOfWork, ILogger<CreatePersonCommandHandler> logger)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"[{nameof(CreatePersonCommandHandler)}] Adding a new person to the repository initiated.");

            var person = _mapper.Map<CreatePersonCommand, Person>(request);
            _personRepository.Create(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"[{nameof(CreatePersonCommandHandler)}] Adding a new person to the repository suceeded.");
            return person.PersonId;
        }
        catch (Exception)
        {
            _logger.LogError($"[{nameof(CreatePersonCommandHandler)}] Adding a new person to the repository failed.");
            throw;
        }
    }
}

