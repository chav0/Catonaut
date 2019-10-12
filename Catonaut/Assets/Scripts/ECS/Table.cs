using System.Collections.Generic;
using UnityEngine;

namespace ECS
{
    public class Table<T> where T : IComponent
    {
        private Dictionary<uint, IComponent> _components; 
        
        public IComponent ComponentAt(uint index)
        {
            return _components[index];
        }
        
        public void CreateAt(uint index, IComponent component)
        {
            _components.Add(index, component); 
        }

        public void DeleteAt(uint index)
        {
            _components.Remove(index); 
        }

        public void Clear()
        {
            _components.Clear();
        }
    }
}
