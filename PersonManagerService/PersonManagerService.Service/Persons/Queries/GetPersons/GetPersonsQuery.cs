using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Abstractions;

namespace PersonManagerService.Application.Queries.GetPersons;

public sealed record GetPersonsQuery() : IQuery<IEnumerable<PersonResponse>>;

