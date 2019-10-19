using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public class Table<T> : IDisposable where T : IComponent
    {
        private readonly List<T>  _components = new List<T>();
        private readonly List<Entity> _entities = new List<Entity>();

        public int Count => _components.Count; 
        
        public T this[int index]
        {
            get => _components[index];
            set => _components[index] = value;
        }

        public Entity EntityAt(int index)
        {
            return _entities[index]; 
        }
        
        public void CreateAt(Entity entity, T component)
        {
            _components.Add(component); 
            _entities.Add(entity);
        }

        public void DeleteAt(Entity entity)
        {
            for (int i = _components.Count - 1; i >= 0; i--)
            {
                if (_entities[i] == entity)
                {
                    _entities.RemoveAt(i);
                    _components.RemoveAt(i);
                }
            }
        }

        public void Dispose()
        {
            _components.Clear();
            _entities.Clear();
        }
    }
}
