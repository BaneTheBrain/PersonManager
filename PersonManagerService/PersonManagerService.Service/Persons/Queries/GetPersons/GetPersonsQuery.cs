using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.DTOs;

namespace PersonManagerService.Domain.Queries.GetPersons;

public sealed record GetPersonsQuery() : IQuery<IEnumerable<PersonResponse>>;

