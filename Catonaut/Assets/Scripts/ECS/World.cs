using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Client.Objects;
using ECS.Components;

namespace ECS
{
    public sealed class World : IDisposable
    {
        private readonly Dictionary<uint, Entity> _entities;
        private uint _lastId = 0; 
        
        public int Tick;
        public Entity ClientEntity; 

        public World()
        {
            _entities = new Dictionary<uint, Entity>();
        }
        
        public Entity this[uint index]
        {
            get => _entities[index];
            set => _entities[index] = value;
        }

        public Entity CreateEntity()
        {
            _lastId++; 
            var newEntity = new Entity(_lastId, this);
            _entities.Add(_lastId, newEntity);
            return newEntity; 
        }

        public void Dispose()
        {
            Players.Dispose();
            Transrofms.Dispose(); 
            Input.Dispose();
            Flashlights.Dispose();
        }

        public Table<Player> Players = new Table<Player>();
        public Table<Transform> Transrofms = new Table<Transform>();
        public Table<Input> Input = new Table<Input>();
        public Table<Flashlight> Flashlights = new Table<Flashlight>();
    }
}
