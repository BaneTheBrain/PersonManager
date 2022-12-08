using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.Models;
using PersonManagerService.Infrastructure.Contexts;

namespace PersonManagerService.Application.Commands.CreatePerson;

public class CreateOrUpdatePersonCommandHandler : IRequestHandler<CreateOrUpdatePersonCommand, Guid>
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrUpdatePersonCommandHandler> _logger;

    public CreateOrUpdatePersonCommandHandler(IUnitOfWork uow, IMapper mapper,ILogger<CreateOrUpdatePersonCommandHandler> logger)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Guid> Handle(CreateOrUpdatePersonCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"{nameof(CreateOrUpdatePersonCommandHandler)} -> Handle request initiated.");

            var person = _mapper.Map<CreateOrUpdatePersonCommand, Person>(request);
            
            if(person.PersonId == Guid.Empty)
            {
                _uow.PersonRepository.Insert(person);
            }
            else
            {
                await _uow.PersonRepository.UpdatePerson(person, cancellationToken);
            }
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"{nameof(CreateOrUpdatePersonCommandHandler)} -> Handle request suceeded.");
            return person.PersonId;
        }
        catch
        {
            _logger.LogError($"{nameof(CreateOrUpdatePersonCommandHandler)} -> Handle request failed.");
            throw;
        }
    }
}

