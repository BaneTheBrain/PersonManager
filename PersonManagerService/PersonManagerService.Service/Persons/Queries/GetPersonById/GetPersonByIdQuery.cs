using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Abstractions;

namespace PersonManagerService.Application.Queries.GetPerson;

public sealed record GetPersonByIdQuery(Guid PersonId) : IQuery<PersonResponse>;

