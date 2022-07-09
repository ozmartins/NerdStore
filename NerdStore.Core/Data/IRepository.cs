﻿using NerdStore.Core.DomainObjects;

namespace NerdStore.Core.Data
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task Commit();
    }
}
