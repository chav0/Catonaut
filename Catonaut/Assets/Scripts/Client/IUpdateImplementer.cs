using System;
using System.Collections.Generic;

namespace Client
{
    public interface IUpdateImplementer<T> : IDisposable
    {
        IEnumerable<uint> GetNextFilteredItem();
        CreationResult<T> Factory(uint entityId);
        void Update(uint entityId, T viewElement);
        void Dispose(uint entityId, T viewElement);
        bool HasEntityWithId(uint entityId);
    }
}