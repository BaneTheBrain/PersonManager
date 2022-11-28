using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonManagerService.Application.Abstractions;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Application.Service;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.DTOs;
using PersonManagerService.Domain.Models;
using PersonManagerService.Domain.Queries.GetPerson;
using PersonManagerService.Domain.Queries.GetPersons;
using PersonManagerService.Domain.Queries.GetSocialMediaAccount;
using Polly.CircuitBreaker;

namespace PersonManagerService.Application.Persons.Queries.GetSocialMediaAccount;

public class GetSocialMediaAccountsQueryHandler : IRequestHandler<GetSocialMediaAccountsQuery, IEnumerable<SocialMediaAccountResponse>>
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<GetSocialMediaAccountsQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetSocialMediaAccountsQueryHandler(IUnitOfWork uow, ILogger<GetSocialMediaAccountsQueryHandler> logger, IMapper mapper)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<SocialMediaAccountResponse>> Handle(GetSocialMediaAccountsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"{nameof(GetSocialMediaAccountsQueryHandler)} -> Handle request initiated.");

            var accounts = _uow.SocialMediaAccountRepository.GetSocialMediaAccounts();
            var retVal = accounts.Select(acnt => _mapper.Map<SocialMediaAccount, SocialMediaAccountResponse>(acnt));

            _logger.LogInformation($"{nameof(GetSocialMediaAccountsQueryHandler)} -> Handle request suceeded.");
            return retVal;
        }
        catch
        {
            _logger.LogError($"{nameof(GetSocialMediaAccountsQueryHandler)} -> Handle request failed.");
            throw;
        }
    }
}
