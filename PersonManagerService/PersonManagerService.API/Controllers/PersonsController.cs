using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonManagerService.Application.Commands.CreatePerson;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Application.Queries.GetPerson;

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
    public async Task<ActionResult<Guid>> CreatePersonAsync(PersonDto personDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"[{nameof(CreatePersonAsync)}] initiated.");
            
            CreatePersonCommand command = new CreatePersonCommand(personDto.FirstName, personDto.LastName, personDto.PersonSkills, personDto.PersonSocialMediaAccounts);
            var personId = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation($"[{nameof(CreatePersonAsync)}] was successfully executed for person: {personDto.FirstName}");
            return personId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[{nameof(CreatePersonAsync)}] failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{personId}")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PersonDto>> GetPersonByIdAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"[{nameof(GetPersonByIdAsync)}] initiated.");

            var query = new GetPersonByIdQuery(personId);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                _logger.LogError($"[{nameof(GetPersonByIdAsync)}] request failed because no data was found for the provided id: {personId}.");
                return NotFound($"No person was found for the id: {personId}.");
            }

            _logger.LogInformation($"[{nameof(GetPersonByIdAsync)}] sucessfully executed.");

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[{nameof(GetPersonByIdAsync)}] failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
