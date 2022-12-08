using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonManagerService.Application.Commands.CreatePerson;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Application.Queries.GetPerson;
using PersonManagerService.Application.Queries.GetPersons;

namespace PersonManagerService.API.Controllers;

/// <summary>
/// Handles Person data
/// </summary>
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

    /// <summary>
    /// Adds new person
    /// </summary>
    /// <param name="personDto">Person data to be added</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles ="admin")]
    public async Task<ActionResult<Guid>> CreatePerson(PersonRequest personDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"{nameof(PersonsController)} -> Create person {personDto.FirstName} initiated.");

            CreateOrUpdatePersonCommand command = new CreateOrUpdatePersonCommand(Guid.Empty, personDto.FirstName, personDto.LastName, personDto.PersonSkills, personDto.PersonSocialMediaAccounts);
            var personId = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation($"{nameof(PersonsController)} -> Create person {personDto.FirstName} succeeded.");
            return CreatedAtAction(nameof(GetPerson), new { id = personId }, personId);
        }
        catch
        {
            _logger.LogError($"{nameof(PersonsController)} -> Create person {personDto.FirstName} failed.");
            throw;
        }
    }

    /// <summary>
    /// Updates person
    /// </summary>
    /// <param name="personDto">Person data to be updated</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    [HttpPut]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[Authorize(Roles ="admin")]
    public async Task<ActionResult<Guid>> UpdatePerson(PersonRequest personDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"{nameof(PersonsController)} -> Update person {personDto.FirstName} initiated.");

            CreateOrUpdatePersonCommand command = new CreateOrUpdatePersonCommand(personDto.PersonId, personDto.FirstName, personDto.LastName, personDto.PersonSkills, personDto.PersonSocialMediaAccounts);
            var personId = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation($"{nameof(PersonsController)} -> Update person {personDto.FirstName} succeeded.");
            return CreatedAtAction(nameof(GetPerson), new { id = personId }, personId);
        }
        catch
        {
            _logger.LogError($"{nameof(PersonsController)} -> Update person {personDto.FirstName} failed.");
            throw;
        }
    }

    /// <summary>
    /// Gets concrete person by provided id
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PersonResponse>> GetPerson(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"{nameof(PersonsController)} -> Get person {id} initiated.");

            var query = new GetPersonByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                _logger.LogError($"{nameof(PersonsController)} -> Person {id} not found");
                return NotFound($"Person {id} not found");
            }

            _logger.LogInformation($"{nameof(PersonsController)} -> Get person {id} succeeded.");
            return Ok(result);
        }
        catch
        {
            _logger.LogError($"{nameof(PersonsController)} -> Get person {id} failed.");
            throw;
        }
    }

    /// <summary>
    /// Gets all persons
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PersonResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PersonResponse>>> GetPersons(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"{nameof(PersonsController)} -> Get persons initiated.");

            var result = await _mediator.Send(new GetPersonsQuery(), cancellationToken);
            if (result is null)
            {
                _logger.LogError($"{nameof(PersonsController)} -> No person found");
                return NotFound($"No person found");
            }

            _logger.LogInformation($"{nameof(PersonsController)} -> Get persons succeeded.");
            return Ok(result);
        }
        catch
        {
            _logger.LogError($"{nameof(PersonsController)} -> Get person failed.");
            throw;
        }
    }
}
