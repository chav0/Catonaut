﻿using Client;
using Client.Scene;
using ECS.Systems;

namespace ECS
{
    public class SystemFactory
    {
        private readonly UnityScene _scene;
        private readonly GameSettings _settings;

        public SystemFactory(UnityScene scene, GameSettings gameSettings)
        {
            _scene = scene;
            _settings = gameSettings; 
        }
        
        public SystemBase[] CreateSystems()
        {
            return new SystemBase[]
            {
                new ProjectileCleanUpSystem(), 

                new FlashlightSystem(_settings), 
                new KeysSystem(_settings),
                new InsertingKeysInCapsuleSystem(_settings),
                
                new FlashlightWeaponSystem(_settings), 
                new WeaponSystem(_settings),
                new ProjectileSystem(),
                new MonsterAttackSystem(_settings),
                new HitsSystem(_settings), 
                new DamageZoneSystem(_settings),
                
                new MonsterMovementSystem(_settings), 
                new MovementSystem(_settings),       
                new BotBehaviourSystem(_settings),
                
                new DeathResolverSystem(),
            };
        }
    }
}