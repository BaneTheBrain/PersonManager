using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.DTOs;
using PersonManagerService.Domain.Queries.GetSocialMediaAccount;

namespace PersonManagerService.API.Controllers
{
    /// <summary>
    /// Handles social media accounts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaAccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SocialMediaAccountsController> _logger;

        public SocialMediaAccountsController(IMediator mediator, ILogger<SocialMediaAccountsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets social media accounts
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SocialMediaAccountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SocialMediaAccountResponse>>> GetAccounts(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation($"{nameof(SocialMediaAccountsController)} -> Get accounts initiated.");

                var result = await _mediator.Send(new GetSocialMediaAccountsQuery(), cancellationToken);
                if (result is null)
                {
                    _logger.LogError($"{nameof(SocialMediaAccountsController)} -> No account found");
                    return NotFound($"No person found");
                }

                _logger.LogInformation($"{nameof(SocialMediaAccountsController)} -> Get accounts succeeded.");
                return Ok(result);
            }
            catch
            {
                _logger.LogError($"{nameof(SocialMediaAccountsController)} -> Get accounts failed.");
                throw;
            }
        }
    }
}
