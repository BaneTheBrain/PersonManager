using PersonManagerService.Application.Abstractions;
using PersonManagerService.Application.DTOs;

namespace PersonManagerService.Application.Queries.GetPersons;

public sealed record GetPersonsQuery() : IQuery<IEnumerable<PersonResponse>>;

