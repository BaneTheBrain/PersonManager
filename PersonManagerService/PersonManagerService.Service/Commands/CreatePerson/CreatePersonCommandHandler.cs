using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.DTOs;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Domain.Commands.CreatePerson;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreatePersonCommandHandler> _logger;


    public CreatePersonCommandHandler(IUnitOfWork uow, IMapper mapper, IUnitOfWork unitOfWork, ILogger<CreatePersonCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
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
            _uow.PersonRepository.Create(person);
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

