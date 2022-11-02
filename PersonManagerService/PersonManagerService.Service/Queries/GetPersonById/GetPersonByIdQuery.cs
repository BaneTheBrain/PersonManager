using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.DTOs;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Domain.Queries.GetPerson;

public sealed record GetPersonByIdQuery(Guid PersonId) : IQuery<PersonResponse>;

