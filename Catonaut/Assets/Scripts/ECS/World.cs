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
        public MapObject Map; 

        public World()
        {
            _entities = new Dictionary<uint, Entity>();
        }
        
        public Entity this[uint index]
        {
            get => _entities[index];
            set => _entities[index] = value;
        }

        public bool HasEntity(uint index)
        {
            return _entities.ContainsKey(index); 
        }

        public Entity CreateEntity()
        {
            _lastId++; 
            var newEntity = new Entity(_lastId, this);
            _entities.Add(_lastId, newEntity);
            return newEntity; 
        }
        
        public void DestroyEntity(Entity entity)
        {
            _entities.Remove(entity.Id); 
            Players.DeleteAt(entity);
            Transrofms.DeleteAt(entity);
            Input.DeleteAt(entity);
            Flashlights.DeleteAt(entity);
            Inventories.DeleteAt(entity);
            Keys.DeleteAt(entity);
            Capsules.DeleteAt(entity);
            Health.DeleteAt(entity);
            SpawnPoints.DeleteAt(entity);
            Weapons.DeleteAt(entity);
            Projectiles.DeleteAt(entity);
        }

        public void Dispose()
        {
            Players.Dispose();
            Transrofms.Dispose(); 
            Input.Dispose();
            Flashlights.Dispose();
            Inventories.Dispose();
            Capsules.Dispose();
            Health.Dispose();
            Keys.Dispose();
            SpawnPoints.Dispose();
            Weapons.Dispose();
        }

        public Table<Player> Players = new Table<Player>();
        public Table<Transform> Transrofms = new Table<Transform>();
        public Table<Input> Input = new Table<Input>();
        public Table<Flashlight> Flashlights = new Table<Flashlight>();
        public Table<Inventory> Inventories = new Table<Inventory>();
        public Table<Key> Keys = new Table<Key>();
        public Table<Capsule> Capsules = new Table<Capsule>();
        public Table<Health> Health = new Table<Health>();
        public Table<SpawnPoint> SpawnPoints = new Table<SpawnPoint>();
        public Table<Weapon> Weapons = new Table<Weapon>();
        public Table<Projectile> Projectiles = new Table<Projectile>();
        public Table<DamageZone> DamageZones = new Table<DamageZone>();

        public Match Match = new Match();
    }
}
