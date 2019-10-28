using System.Collections.Generic;
using Client;
using Client.Objects;
using Client.Scene;
using UnityEngine;

namespace ECS
{
    public class GameLogic
    {
        private const int PlayerCount = 2;
        private readonly SystemFactory _factory;
        private readonly GameSettings _gameSettings;
        private readonly UnityScene _scene;
        private SystemBase[] _systems;
        private World _world;
        
        public GameLogic(SystemFactory factory, GameSettings settings, UnityScene scene)
        {
            _factory = factory;
            _gameSettings = settings; 
            _scene = scene;
        }
        
        public void Init(World world, int tickRate)
        {
            _world = world;
            
            _systems = _factory.CreateSystems();
            for (int i = 0, count = _systems.Length; i < count; i++)
            {
                _systems[i].Init(world, tickRate);
            }
        }
        
        public void Simulate(Input input)
        {
            _world.ClientEntity.Input = input;
            
            _world.Tick++;
            for (int i = 0, count = _systems.Length; i < count; i++)
            {
                _systems[i].Execute();
            }
        }

        public World CreateNewWorld(int tickRate)
        {
            var world = new World();

            var map = _scene.CreateMap();
            world.Map = map; 

            foreach (var spawnZoneBody in map.SpawnZones)
            {
                var entity = world.CreateEntity();
                var spawnPoint = entity.AddSpawnPoint();
                spawnPoint.Position = spawnZoneBody.position;
                spawnPoint.Rotation = spawnZoneBody.rotation;
            }
            
            for (int i = 0; i < PlayerCount; i++)
            {
                var playerEntity = world.CreateEntity();
                
                var player = playerEntity.AddPlayer();
                
                var transform = playerEntity.AddTransform();
                transform.Position = world.SpawnPoints[i].Position;
                transform.Rotation = world.SpawnPoints[i].Rotation;

                playerEntity.AddFlashlight();
                playerEntity.AddInventory();
                playerEntity.AddInput(); 
                
                var health = playerEntity.AddHealth();
                health.MaxHealth = _gameSettings.MaxPlayerHealth;
                health.CurrentHealth = health.MaxHealth;

                var weapon = playerEntity.AddWeapon();
                weapon.ChargeTime = (int) (_gameSettings.WeaponChargeTime * tickRate); 
                weapon.CooldownTime = (int) (_gameSettings.WeaponCooldownTime * tickRate); 

                var body = _scene.CreatePlayer();
                body.Entity = playerEntity;
                player.PlayerObject = body; 
                
                if (i == 0)
                {
                    world.ClientEntity = playerEntity;
                }
            }

            foreach (var keyBody in map.Keys)
            {
                var keyEntity = world.CreateEntity();
                keyBody.Entity = keyEntity; 
                var key = keyEntity.AddKey();
                key.Body = keyBody;
                key.KeyColor = keyBody.KeyColor;
            }

            var capsuleBody = map.Capsule;
            var capsuleEntity = world.CreateEntity();
            capsuleBody.Entity = capsuleEntity;
            var capsule = capsuleEntity.AddCapsule();
            capsuleEntity.AddInventory(); 
            capsule.RequiredKeys.AddRange(capsuleBody.RequiredKeys);
            capsule.Body = capsuleBody;

            foreach (var damageZoneBody in map.DamageZoneObjects)
            {
                var damageZoneEntity = world.CreateEntity();
                var damageZone = damageZoneEntity.AddDamageZone();
                var damageZoneHealth = damageZoneEntity.AddHealth();
                damageZone.Damage = damageZoneBody.Damage;
                damageZone.Position = damageZoneBody.transform.position;
                damageZone.Radius = damageZoneBody.Radius;
                damageZone.Body = damageZoneBody; 
                damageZoneHealth.CurrentHealth = damageZoneBody.Health;
                damageZoneHealth.MaxHealth = damageZoneBody.Health;
                damageZoneBody.Entity = damageZoneEntity; 
            }

            foreach (var monsterBody in map.Monsters)
            {
                var monsterEntity = world.CreateEntity();
                var monster = monsterEntity.AddMonster();
                var monsterHealth = monsterEntity.AddHealth();
                monster.Body = monsterBody; 
                monsterHealth.CurrentHealth = monsterBody.Health;
                monsterHealth.MaxHealth = monsterBody.Health;
                monsterBody.Entity = monsterEntity;
                monsterBody.transform.position = monsterBody.Points[0].position; 
            }
            
            return world;
        }
    }
}
