using PersonManagerService.Application.Abstractions;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Models;

namespace PersonManagerService.Application.Queries.GetPerson;

public sealed record GetPersonByIdQuery(Guid PersonId) : IQuery<PersonResponse>;

