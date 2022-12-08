using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Application.Queries.GetSocialMediaAccount;

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
