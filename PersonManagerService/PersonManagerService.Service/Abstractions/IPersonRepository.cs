﻿using PersonManagerService.Domain.Models;

namespace PersonManagerService.Application.Abstractions
{
    public interface IPersonRepository
    {
        void Create(Person person);
    }
}
