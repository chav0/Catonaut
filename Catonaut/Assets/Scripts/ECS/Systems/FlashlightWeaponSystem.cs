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
                var overlaps = PhysicsUtils.OverlapSphere(transform.Position, flashlight.Range/2.75f, 
                    Layers.FlashlightEnemysMask | Layers.MonstersMask);

                foreach (var overlap in overlaps)
                {
                    var monster = overlap.Monster;
                    if (monster != null && monster.HaveShield && World.Tick % _gameSettings.DamageTicks == 0)
                    {
                        monster.CurrentShieldHealth -= 1; 
                        
                        if(monster.CurrentShieldHealth == 0)
                        {
                            monster.HaveShield = false;
                            monster.DamageCoef = 1f;
                            monster.Body.Shield.gameObject.SetActive(false);
                        }
                    }
                    
                    if(monster != null)
                        continue;
                    
                    var health = overlap.Health;
                    if(health != null)
                    {
                        if (World.Tick % _gameSettings.DamageTicks == 0)
                        {
                            health.CurrentHealth -= _gameSettings.FlashlightDamage;
                            if (health.CurrentHealth < 0)
                                health.CurrentHealth = 0;
                        }
                        continue;
                    }
                }
            }
        }
    }
}
