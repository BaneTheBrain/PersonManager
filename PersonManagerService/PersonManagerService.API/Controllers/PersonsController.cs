using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonManagerService.Application.Commands.CreatePerson;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Application.Queries.GetPerson;
using PersonManagerService.Application.Queries.GetPersons;

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
            
            CreatePersonCommand command = new CreatePersonCommand(personDto.FirstName, personDto.LastName, personDto.PersonSkills, personDto.PersonSocialMediaAccounts);
            var personId = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation($"[{nameof(CreatePerson)}] was successfully executed for person: {personDto.FirstName}");
             return CreatedAtAction(nameof(GetPerson), new { id = personId }, personId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[{nameof(CreatePerson)}] failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PersonDto>> GetPerson(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"[{nameof(GetPerson)}] initiated.");

            var query = new GetPersonByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                _logger.LogError($"[{nameof(GetPerson)}] request failed because no data was found for the provided id: {id}.");
                return NotFound($"No person was found for the id: {id}.");
            }

            _logger.LogInformation($"[{nameof(GetPerson)}] sucessfully executed.");

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[{nameof(GetPerson)}] failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PersonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"[{nameof(GetPersons)}] initiated.");

            var result = await _mediator.Send(new GetPersonsQuery(), cancellationToken);
            if(result is null)
            {
                return NotFound($"No person was found.");
            }

            _logger.LogInformation($"[{nameof(GetPersons)}] sucessfully executed.");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[{nameof(GetPersons)}] failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
