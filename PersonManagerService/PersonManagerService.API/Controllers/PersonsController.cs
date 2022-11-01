using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonManagerService.Application.Commands.CreatePerson;
using PersonManagerService.Application.DTOs;

namespace PersonManagerService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PersonsController> _logger;

    public PersonsController(IMediator mediator, ILogger<PersonsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Guid>> CreatePerson(PersonDto personDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"[{nameof(CreatePerson)}] initiated.");
            
            CreatePersonCommand command = new CreatePersonCommand(personDto.FirstName, personDto.LastName, personDto.Skills, personDto.SocialMediaAccounts);
            var personId = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation($"[{nameof(CreatePerson)}] was successfully executed for person: {personDto.FirstName}");
            return CreatedAtAction(nameof(CreatePerson), new { personId }, personId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[{nameof(CreatePerson)}] failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
