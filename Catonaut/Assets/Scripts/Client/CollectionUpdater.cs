using System.Collections.Generic;

namespace Client
{
    public class CollectionUpdater<T> 
    {
        private readonly List<KeyValuePair<uint, T>> _entities = new List<KeyValuePair<uint, T>>();
        private readonly Dictionary<uint, T> _entitiesAsDict = new Dictionary<uint, T>();

        public void ProcessUpdate(IUpdateImplementer<T> updater)
        {
            RemoveDeletedItems(updater);
            CreateNewItems(updater);
        }

        public void Dispose(IUpdateImplementer<T> updater)
        {
            for (var index = 0; index < _entities.Count; index++)
            {
                var item = _entities[index];
                updater.Dispose(item.Key, item.Value);
            }

            _entities.Clear();
            _entitiesAsDict.Clear();
        }

        public T GetById(uint id)
        {
            T result;
            _entitiesAsDict.TryGetValue(id, out result);
            return result;
        }

        private void RemoveDeletedItems(IUpdateImplementer<T> updater)
        {
            for (var index = 0; index < _entities.Count; index++)
            {
                var kvpCreated = _entities[index];

                if (!updater.HasEntityWithId(kvpCreated.Key))
                {
                    var item = _entities[index];
                    updater.Dispose(item.Key, item.Value);
                    _entities.RemoveAt(index);
                    _entitiesAsDict.Remove(kvpCreated.Key);
                    index--;
                }
            }
        }

        private void CreateNewItems(IUpdateImplementer<T> updater)
        {
            foreach (var entity in updater.GetNextFilteredItem())
            {
                T val;
                var exist = _entitiesAsDict.TryGetValue(entity, out val);

                if (!exist)
                {
                    var crResult = updater.Factory(entity);
                    if (crResult.IsCreated)
                    {
                        _entities.Add(new KeyValuePair<uint, T>(entity, crResult.Result));
                        _entitiesAsDict.Add(entity, crResult.Result);
                        val = crResult.Result;
                        exist = true;
                    }
                }

                if (exist)
                    updater.Update(entity, val);

            }
        }
    }

    public struct CreationResult<T>
    {
        public bool IsCreated;
        public T Result;
    }
}