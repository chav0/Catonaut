﻿using Client;
using ECS.Physics;
using UnityEngine;

namespace ECS.Systems
{
    public class FlashlightWeaponSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
    
        public FlashlightWeaponSystem(GameSettings settings)
        {
            _gameSettings = settings;
        }
        
        public override void Execute()
        {
            for (int i = 0; i < World.Flashlights.Count; i++)
            {
                var entity = World.Flashlights.EntityAt(i);
                
                var flashlight = entity.Flashlight;
                var transform = entity.Transform;
                var overlaps = PhysicsUtils.OverlapSphere(transform.Position, flashlight.Range/4, Layers.FlashlightEnemysMask);

                foreach (var overlap in overlaps)
                {
                    var health = overlap.Health;
                    if(health == null)
                        continue;

                    if (World.Tick % _gameSettings.DamageTicks == 0)
                    {
                        health.CurrentHealth -= _gameSettings.FlashlightDamage;
                        if (health.CurrentHealth < 0) 
                            health.CurrentHealth = 0;
                    }
                }
            }
        }
    }
}
